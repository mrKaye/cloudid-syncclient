using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserSyncClient
{
    /// <summary>
    /// This executable will be used for scheduling
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Process runningProcess = Process.GetCurrentProcess();
                Process[] pname = Process.GetProcessesByName(runningProcess.ProcessName);
                if (pname.Length > 1)
                    return;

                //Getting the security group of active directory whose users needs to be synced with cloud
                string securityGroup = ConfigurationManager.AppSettings["SecurityGroup"];

                string domainPath = ConfigurationManager.AppSettings["LDAPPath"];

                if (string.IsNullOrEmpty(domainPath))
                {
                    new ExceptionHandler("LDAP path is not configured");
                    return;
                }
                DirectoryEntry searchRoot = null;
                searchRoot = new DirectoryEntry(domainPath);
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ADServerName"]))
                {
                    searchRoot.Username = ConfigurationManager.AppSettings["ADServerUserName"];
                    searchRoot.Password = ConfigurationManager.AppSettings["ADServerPassword"];
                }
                
                DirectorySearcher search = new DirectorySearcher(searchRoot, "(&(objectCategory=group)(CN=cloudidsyncusers))");
                SearchResult result = search.FindOne();

                if (result != null)
                {
                    List<SecurityGroupUser> users = new List<SecurityGroupUser>();
                    string adServerName = ConfigurationManager.AppSettings["ADServerName"];
                    foreach (var member in result.Properties["member"])
                    {
                        DirectoryEntry userDe = null;
                        if (!string.IsNullOrEmpty(adServerName))
                        {
                            userDe = new DirectoryEntry(String.Concat("LDAP://", adServerName, "/", member.ToString()));
                            userDe.Username = ConfigurationManager.AppSettings["ADServerUserName"];
                            userDe.Password = ConfigurationManager.AppSettings["ADServerPassword"];
                        }
                        {
                            userDe = new DirectoryEntry(String.Concat("LDAP://", member.ToString()));
                        }

                        SecurityGroupUser adUser = new SecurityGroupUser();
                        if (userDe.Properties["objectClass"].Contains("user"))
                        {
                            if ((userDe.Properties.Contains("objectSid")) && (userDe.Properties["objectSid"].Count > 0))
                            {
                                SecurityIdentifier siSid = new SecurityIdentifier((byte[])userDe.Properties["objectSid"][0], 0);
                                adUser.Sid = siSid.ToString();
                            }
                            if ((userDe.Properties.Contains("userPrincipalName")) && (userDe.Properties["userPrincipalName"].Count > 0))
                            {
                                adUser.Upn = Convert.ToString(userDe.Properties["userPrincipalName"][0]);
                            }
                            if ((userDe.Properties.Contains("whenChanged")) && (userDe.Properties["whenChanged"].Count > 0))
                            {
                                adUser.TimeStamp = Convert.ToDateTime(userDe.Properties["whenChanged"][0]);
                            }
                            if ((userDe.Properties.Contains("sAMAccountName")) && (userDe.Properties["sAMAccountName"].Count > 0))
                            {
                                adUser.SamAccountName = Convert.ToString(userDe.Properties["sAMAccountName"][0]);
                            }
                            if ((userDe.Properties.Contains("displayName")) && (userDe.Properties["displayName"].Count > 0))
                            {
                                adUser.DisplayName = Convert.ToString(userDe.Properties["displayName"][0]);
                            }
                            if ((userDe.Properties.Contains("sn")) && (userDe.Properties["sn"].Count > 0))
                            {
                                adUser.LastName = Convert.ToString(userDe.Properties["sn"][0]);
                            }
                            if ((userDe.Properties.Contains("givenName")) && (userDe.Properties["givenName"].Count > 0))
                            {
                                adUser.FirstName = Convert.ToString(userDe.Properties["givenName"][0]);
                            }
                            if ((userDe.Properties.Contains("description")) && (userDe.Properties["description"].Count > 0)
                                && (string.IsNullOrEmpty(ConfigurationManager.AppSettings["description"]) ? true : Convert.ToBoolean(ConfigurationManager.AppSettings["description"])))
                            {
                                adUser.Description = Convert.ToString(userDe.Properties["description"][0]);
                            }
                            if ((userDe.Properties.Contains("l")) && (userDe.Properties["l"].Count > 0)
                                && (string.IsNullOrEmpty(ConfigurationManager.AppSettings["l"]) ? true : Convert.ToBoolean(ConfigurationManager.AppSettings["l"])))
                            {
                                adUser.City = Convert.ToString(userDe.Properties["l"][0]);
                            }
                            if ((userDe.Properties.Contains("streetAddress")) && (userDe.Properties["streetAddress"].Count > 0)
                                && (string.IsNullOrEmpty(ConfigurationManager.AppSettings["streetAddress"]) ? true : Convert.ToBoolean(ConfigurationManager.AppSettings["streetAddress"])))
                            {
                                adUser.Street = Convert.ToString(userDe.Properties["streetAddress"][0]);
                            }
                            if ((userDe.Properties.Contains("postalCode")) && (userDe.Properties["postalCode"].Count > 0)
                                && (string.IsNullOrEmpty(ConfigurationManager.AppSettings["postalCode"]) ? true : Convert.ToBoolean(ConfigurationManager.AppSettings["postalCode"])))
                            {
                                adUser.ZipCode = Convert.ToString(userDe.Properties["postalCode"][0]);
                            }
                            if ((userDe.Properties.Contains("mail")) && (userDe.Properties["mail"].Count > 0)
                                && (string.IsNullOrEmpty(ConfigurationManager.AppSettings["mail"]) ? true : Convert.ToBoolean(ConfigurationManager.AppSettings["mail"])))
                            {

                                char[] specialChars = { ',', '/', ';', ':', '-' };
                                string[] arrEmails = Convert.ToString(userDe.Properties["mail"][0]).Split(specialChars);
                                if (!Regex.IsMatch(arrEmails[0], @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                                                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase))
                                {
                                    new ExceptionHandler(new Exception("Invalid Email ID"));
                                }
                                EmailDetail email = new EmailDetail();
                                email.Email = arrEmails[0];
                                email.IsPrimary = true;
                                adUser.EmailAddresses = new[] { email };
                            }
                            if ((userDe.Properties.Contains("telephoneNumber")) && (userDe.Properties["telephoneNumber"].Count > 0)
                                && (string.IsNullOrEmpty(ConfigurationManager.AppSettings["telephoneNumber"]) ? true : Convert.ToBoolean(ConfigurationManager.AppSettings["telephoneNumber"])))
                            {
                                adUser.PhoneNumber = Convert.ToString(userDe.Properties["telephoneNumber"][0]);
                            }
                            if ((userDe.Properties.Contains("mobile")) && (userDe.Properties["mobile"].Count > 0)
                                && (string.IsNullOrEmpty(ConfigurationManager.AppSettings["mobile"]) ? true : Convert.ToBoolean(ConfigurationManager.AppSettings["mobile"])))
                            {
                                adUser.MobileNumber = Convert.ToString(userDe.Properties["mobile"][0]);
                            }
                        }
                        users.Add(adUser);
                    }
                    result = null;
                    StoreDataLocally(users);
                }

                DateTime dt1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 30, 0);
                DateTime dt2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                TimeSpan ts = (dt2 - dt1);
                if (ts.TotalSeconds > 0 && ts.TotalSeconds < 900)
                {
                    var releaseDetails = new CloudOperation().GetReleaseVersion();
                    if (releaseDetails != null && !string.IsNullOrEmpty(releaseDetails.LatestVersion))
                    {
                        string versionFromApi = releaseDetails.LatestVersion;
                        string appVersion = ConfigurationManager.AppSettings["Version"];
                        if (versionFromApi.CompareTo(appVersion) > 0)
                        {
                            EmailNotification notification = new EmailNotification()
                            {
                                CustomerShortCode = ConfigurationManager.AppSettings["CustomerShortCode"],
                                NotificationEmail = ConfigurationManager.AppSettings["NotificationEmail"],
                                Release = ConfigurationManager.AppSettings["Version"]
                            };
                            new CloudOperation().NotifyCustomer(notification);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ExceptionHandler(ex);
                return;
            }
        }

        /// <summary>
        /// This method is used to store the data into local storage after updating into cloud
        /// </summary>
        /// <param name="users"></param>
        static void StoreDataLocally(List<SecurityGroupUser> users)
        {
            //Get the local storage data path
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SecurityGroupUser.xml");
            if (!System.IO.File.Exists(filePath))
            {
                new ExceptionHandler(new Exception("Local Repository XML file not found"));
                return;
            }
            DataSet ds = new DataSet();
            ds.ReadXml(filePath);
            List<CloudData> cloudUserData = new List<CloudData>();
            CloudOperation operation = new CloudOperation();
            try
            {
                if (ds.Tables.Count > 0)
                {
                    List<SecurityGroupUser> userToAdd = new List<SecurityGroupUser>();
                    List<string> sidToDelete = new List<string>();
                    List<SecurityGroupUser> userToUpdate = new List<SecurityGroupUser>();

                    DataSet dsTemp = ds.Copy();

                    foreach (DataRow dr in dsTemp.Tables[0].Rows)
                    {

                        SecurityGroupUser user = users.Find(x => x.Sid == dr["sid"].ToString());

                        if (user == null)
                        {
                            //Delete the user from the cloud
                            if (operation.DeleteUserFromCloud(new UserDetail()
                            {
                                Upn = dr["UPN"].ToString(),
                                UserSid = dr["sid"].ToString(),
                                CustomerShortCode = ConfigurationManager.AppSettings["CustomerShortCode"],
                                ApiUserName = ConfigurationManager.AppSettings["UserName"],
                                ApiPassWord = ConfigurationManager.AppSettings["Password"]
                            }))
                            {
                                //Delete the user from the xml file
                                DataRow dRow = ds.Tables[0].Select("sid='" + dr["sid"].ToString() + "'").FirstOrDefault();
                                ds.Tables["User"].Rows.Remove(dRow);
                            }
                        }
                        else
                        {
                            DateTime localStorageTime = Convert.ToDateTime(dr["timestamp"]);
                            DateTime ADUserChangedTime = Convert.ToDateTime(user.TimeStamp);
                            TimeSpan ts = ADUserChangedTime - localStorageTime;

                            if (ts.TotalMilliseconds > 0)
                            {
                                if (!((string.IsNullOrEmpty(ConfigurationManager.AppSettings["SyncOption"])) || (ConfigurationManager.AppSettings["SyncOption"].ToLower() == "userprincipalname") || (ConfigurationManager.AppSettings["SyncOption"].ToLower() != "mail")))
                                {
                                    user.Upn = user.EmailAddresses[0].Email;
                                }
                                
                                if (operation.AddUpdateUserToCloud(BuildCloudData(user), "/api/User/update"))
                                {
                                    //Update the user in xml file to be added into cloud
                                    userToUpdate.Add(user);
                                    ds.Tables["User"].Select("sid='" + dr["sid"].ToString() + "'")[0]["upn"] = user.Upn;
                                    ds.Tables["User"].Select("sid='" + dr["sid"].ToString() + "'")[0]["timestamp"] = user.TimeStamp;
                                }
                            }
                        }
                    }
                    foreach (SecurityGroupUser userFromAD in users)
                    {
                        if (ds.Tables["User"].Select("sid='" + userFromAD.Sid + "'").FirstOrDefault() == null)
                        {
                            if (operation.AddUpdateUserToCloud(BuildCloudData(userFromAD), "/api/User/create"))
                            {
                                DataRow addNewRow = ds.Tables[0].NewRow();
                                addNewRow["sid"] = userFromAD.Sid;
                                if ((string.IsNullOrEmpty(ConfigurationManager.AppSettings["SyncOption"])) || (ConfigurationManager.AppSettings["SyncOption"].ToLower() == "userprincipalname") || (ConfigurationManager.AppSettings["SyncOption"].ToLower() != "mail"))
                                    addNewRow["upn"] = userFromAD.Upn;
                                else if (ConfigurationManager.AppSettings["SyncOption"].ToLower() == "mail")
                                {
                                    addNewRow["upn"] = userFromAD.EmailAddresses[0].Email;
                                }
                                addNewRow["timestamp"] = userFromAD.TimeStamp;
                                ds.Tables["User"].Rows.Add(addNewRow);
                            }
                        }
                    }
                }
                else if (users.Count > 0)
                {
                    DataTable dt = new DataTable("User");
                    dt.Columns.Add("sid");
                    dt.Columns.Add("upn");
                    dt.Columns.Add("timestamp");
                    foreach (SecurityGroupUser userFromAD in users)
                    {
                        //cloudUserData.Add(BuildCloudData(userFromAD));
                        if (operation.AddUpdateUserToCloud(BuildCloudData(userFromAD), "/api/User/create"))
                        {
                            DataRow dr = dt.NewRow();
                            dr["sid"] = userFromAD.Sid;
                            if ((string.IsNullOrEmpty(ConfigurationManager.AppSettings["SyncOption"])) || (ConfigurationManager.AppSettings["SyncOption"].ToLower() == "userprincipalname") || (ConfigurationManager.AppSettings["SyncOption"].ToLower() != "mail"))
                                dr["upn"] = userFromAD.Upn;
                            else if (ConfigurationManager.AppSettings["SyncOption"].ToLower() == "mail")
                            {
                                dr["upn"] = userFromAD.EmailAddresses[0].Email;
                            }
                            dr["timestamp"] = userFromAD.TimeStamp;
                            dt.Rows.Add(dr);
                        }
                    }
                    ds.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {
                new ExceptionHandler(ex);
            }
            //Update the local storage
            ds.WriteXml(filePath);
        }

        /// <summary>
        /// This method is used to get the metadata properties for the ad users.
        /// </summary>
        /// <param name="cloudUser"></param>
        /// <returns>ad user metadata</returns>
        static CloudData BuildCloudData(SecurityGroupUser cloudUser)
        {
            CloudData data = new CloudData();

            if (ConfigurationManager.AppSettings["SyncOption"].ToLower() != "userprincipalname" && ConfigurationManager.AppSettings["SyncOption"].ToLower() != "mail")
            {
                throw new ApplicationException("Syncing option can be 'userPrincipalName' or 'mail'");
            }
            else if (ConfigurationManager.AppSettings["SyncOption"].ToLower() == "userprincipalname")
            {
                data.Upn = cloudUser.Upn;
            }
            else if (ConfigurationManager.AppSettings["SyncOption"].ToLower() == "mail")
            {
                if (cloudUser.EmailAddresses == null)
                {
                    return null;
                }
                data.Upn = cloudUser.EmailAddresses[0].Email;
            }
            data.CustomerShortCode = ConfigurationManager.AppSettings["CustomerShortCode"];
            data.ApiUserName = ConfigurationManager.AppSettings["UserName"];
            data.ApiPassWord = ConfigurationManager.AppSettings["Password"];
            data.SamAccountName = cloudUser.SamAccountName;
            data.Description = string.IsNullOrEmpty(cloudUser.Description) ? "" : cloudUser.Description;
            data.FirstName = string.IsNullOrEmpty(cloudUser.FirstName) ? "" : cloudUser.FirstName;
            data.LastName = string.IsNullOrEmpty(cloudUser.LastName) ? "" : cloudUser.LastName;
            data.DisplayName = string.IsNullOrEmpty(cloudUser.DisplayName) ? "" : cloudUser.DisplayName;
            data.UserSid = string.IsNullOrEmpty(cloudUser.Sid) ? "" : cloudUser.Sid;
            data.EmailAddresses = (cloudUser.EmailAddresses == null || cloudUser.EmailAddresses.Length == 0) ? new EmailDetail[] { new EmailDetail() { Email = data.Upn, IsPrimary = true } } : cloudUser.EmailAddresses;
            //data.Address = string.IsNullOrEmpty(cloudUser.Address) ? "" : cloudUser.Address;
            data.Telephone = string.IsNullOrEmpty(cloudUser.PhoneNumber) ? "" : cloudUser.PhoneNumber;
            //data.MobileNumber = string.IsNullOrEmpty(cloudUser.MobileNumber) ? "" : cloudUser.MobileNumber;
            data.Zip = string.IsNullOrEmpty(cloudUser.ZipCode) ? "" : cloudUser.ZipCode;
            data.City = string.IsNullOrEmpty(cloudUser.City) ? "" : cloudUser.City;
            data.Street = string.IsNullOrEmpty(cloudUser.Street) ? "" : cloudUser.Street;
            data.PassWord = string.IsNullOrEmpty(cloudUser.Password) ? "" : cloudUser.Password;
            return data;
        }
    }
}
