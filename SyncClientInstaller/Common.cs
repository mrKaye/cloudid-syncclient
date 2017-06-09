using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Dynamic;
using System.Net;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.DirectoryServices;

namespace SyncClientInstaller
{
    public class Common
    {
        public static void AddAppSettingData(string key, string value)
        {
            Dictionary<string, string> dictAppSettingsdata = GlobalData.AppSettingsData;
            if (dictAppSettingsdata == null)
            {
                dictAppSettingsdata = new Dictionary<string, string>();
            }
            if (dictAppSettingsdata.ContainsKey(key))
            {
                dictAppSettingsdata[key] = value;
            }
            else
            {
                dictAppSettingsdata.Add(key, value);
            }
            GlobalData.AppSettingsData = dictAppSettingsdata;
        }

        public static string GetAppSettingdataValue(string key)
        {
            Dictionary<string, string> dictAppSettingsdata = GlobalData.AppSettingsData;
            if (dictAppSettingsdata != null)
            {
                if (dictAppSettingsdata.ContainsKey(key))
                {
                    return dictAppSettingsdata[key];
                }
            }
            return "";
        }

        public static KeyValueConfigurationCollection GetPreviousAppSettings()
        {
            string previousVersionPath = Scheduler.GetScheduledTaskPath("SyncClientApp");
            if (!string.IsNullOrEmpty(previousVersionPath))
            {
                var configPrevVersion = ConfigurationManager.OpenExeConfiguration(String.Format(@"{0}\UserSyncClient.exe", Path.GetDirectoryName(previousVersionPath)));

                return configPrevVersion.AppSettings.Settings;
            }
            else
            {
                return null;
            }
        }
        public static void UpdateConfigFile()
        {
            Dictionary<string, string> appSettingsData = GlobalData.AppSettingsData;
            if (appSettingsData != null && appSettingsData.Count > 0)
            {
                Configuration configApp = null;
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = "Data/UserSyncClient.exe.config";
                //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configApp = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                var settings = configApp.AppSettings.Settings;
                var prevConfiguration = GetPreviousAppSettings();
                if (prevConfiguration == null)
                {
                    foreach (var data in appSettingsData)
                    {
                        if (settings[data.Key] != null)
                        {
                            settings.Remove(data.Key);
                        }
                        settings.Add(data.Key, data.Value);
                    }
                }
                else
                {
                    if (settings["Version"] != null)
                        settings.Remove("Version");
                    settings.Add("Version", System.Reflection.Assembly.GetAssembly(typeof(SyncClientApp)).GetName().Version.ToString());
                    foreach(var prevData in prevConfiguration.AllKeys)
                    {
                        if (settings[prevData] != null)
                        {
                            settings.Remove(prevData);
                        }
                        settings.Add(prevData, prevConfiguration[prevData].Value);
                    }
                }
                if (settings["SecurityGroup"] != null)
                {
                    settings.Remove("SecurityGroup");
                }
                settings.Add("SecurityGroup", "cloudidsyncusers");
                configApp.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configApp.AppSettings.SectionInformation.Name);
            }
        }

        public static void CheckSecurityGroupExistance()
        {
            using(DirectoryEntry baseLDAPDirectoryEntry=new DirectoryEntry(Common.GetAppSettingdataValue("LDAPPath")))
            {
                if(!string.IsNullOrEmpty(Common.GetAppSettingdataValue("ADServerName")))
                {
                    baseLDAPDirectoryEntry.Username = Common.GetAppSettingdataValue("ADServerUserName");
                    baseLDAPDirectoryEntry.Password = Common.GetAppSettingdataValue("ADServerPassword");
                }
                using (DirectorySearcher search = new DirectorySearcher(baseLDAPDirectoryEntry, "(&(objectCategory=group)(CN=cloudidsyncusers))"))
                {
                    if (search.FindOne() == null)
                    {
                        using (DirectoryEntry groupEntry = new DirectoryEntry(GlobalData.OUPath))
                        {
                            if (!string.IsNullOrEmpty(Common.GetAppSettingdataValue("ADServerName")))
                            {
                                groupEntry.Username = Common.GetAppSettingdataValue("ADServerUserName");
                                groupEntry.Password = Common.GetAppSettingdataValue("ADServerPassword");
                            }
                            using (DirectoryEntry group = groupEntry.Children.Add("CN=cloudidsyncusers", "group"))
                            {
                                group.Properties["sAmAccountName"].Value = "cloudidsyncusers";
                                group.Properties["description"].Value = "Members of this group will be synced to cloud";
                                group.CommitChanges();
                            }
                        }
                    }
                }
            }            
        }

        public static string GetResourceKeyValue(string key)
        {
            ResourceManager resManager = new ResourceManager(typeof(SyncClientInstaller.Resource.SyncClientResource));
            return resManager.GetString(key, new CultureInfo(GlobalData.Language));
        }

        public static WebResponse GetApiResponse(string userName, string password, string apiFunction, string requestMethod, string contentType, string content)
        {
            WebResponse response = null;
            HttpWebRequest request = null;
            try
            {
                var apiUrl = new Uri(Common.GetAppSettingdataValue("ApiBaseUrl") + apiFunction);
                var encodedCredential = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(userName + ":" + password));
                request = (HttpWebRequest)HttpWebRequest.Create(apiUrl);
                request.Method = requestMethod;
                request.ContentType = contentType;
                request.Accept = contentType;
                request.Headers.Add("Authorization", "Basic " + encodedCredential);
                if (!string.IsNullOrEmpty(content))
                {
                    using (var writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(content);
                    }
                }

                response = request.GetResponse();
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
                if (request != null)
                    request.Abort();
            }
        }

        public static void GetAllUsers()
        {
            var username = "Syn";
            var password = "768d7eb3-d541-446c-9a24-4b9f935fcd74";
            var encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

            var request =
                   (HttpWebRequest)
                       HttpWebRequest.Create("http://cloudidsyncapi.infostorm.no/api/customer/validate");

            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Basic " + encoded);

            var streamProperties = new
            {
                CustomerShortCode = "Syn",
                ApiUserName = "Subodh@SyncClient.com",
                ApiPassWord = "Span@1234"
            };

            var content = Json.Encode(streamProperties);

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(content);
            }
            try
            {
                var response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var responseContent = reader.ReadToEnd();
                dynamic result = Json.Decode(responseContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //using (var reader = new StreamReader(response.GetResponseStream()))
            //{
            //    var responseContent = reader.ReadToEnd();
            //    var result = Json.Decode(responseContent);
            //}
        }
    }
}
