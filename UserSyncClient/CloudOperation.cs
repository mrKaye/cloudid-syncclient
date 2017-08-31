using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace UserSyncClient
{
    /// <summary>
    /// This class is used to perform the cloud opetation
    /// </summary>
    public class CloudOperation
    {
        /// <summary>
        /// This method is used to create the request
        /// </summary>
        /// <param name="requestMethod">Either Get or Post</param>
        /// <param name="contentType">ContentType Http Header</param>
        /// <param name="apiFunction">api method to invoke</param>
        /// <param name="content">JSON body</param>
        /// <returns>Http request</returns>
        public HttpWebRequest GetCloudRequest(string requestMethod, string contentType, string apiFunction, string content)
        {
            HttpWebRequest request = null;
            try
            {
                var apiUrl = new Uri(ConfigurationManager.AppSettings["ApiBaseUrl"] + apiFunction);
                var encodedCredential = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(ConfigurationManager.AppSettings["CustomerShortCode"] + ":" + ConfigurationManager.AppSettings["ApiKey"]));
                request = (HttpWebRequest)HttpWebRequest.Create(apiUrl);
                request.Method = requestMethod;
                request.ContentType = contentType;
                request.Accept = contentType;
                request.Headers.Add("Authorization", "Basic " + encodedCredential);
                request.Timeout = 9999999;
                if (!string.IsNullOrEmpty(content))
                {
                    using (var writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(content);
                    }
                }
                return request;
            }
            catch (Exception ex)
            {
                new ExceptionHandler(ex);
            }
            return null;
        }

        /// <summary>
        /// This method is used to create a new user or update the existing user
        /// </summary>
        /// <param name="data">data to update with respect to new or existing user</param>
        /// <param name="apiMethod">api method to invoke</param>
        /// <returns>bool</returns>
        public bool AddUpdateUserToCloud(SecurityGroupUser userData, string apiMethod)
        {
            try
            {
                CloudData data = BuildCloudData(userData);
                if (data != null)
                {
                    bool isUserCreated = false;
                    var content = new JavaScriptSerializer().Serialize(data);
                    
                    if (!string.IsNullOrEmpty(content))
                    {
                        string responseStr = GetAndCheckResponse(GetCloudRequest("POST", "application/json", apiMethod, content));

                        if (string.IsNullOrEmpty(responseStr))
                            return false;
                        if (responseStr == "failed")
                        {
                            string retryResponseStr = GetAndCheckResponse(GetCloudRequest("POST", "application/json", apiMethod, content));
                            if (string.IsNullOrEmpty(retryResponseStr))
                                return false;
                            if (retryResponseStr == "failed")
                            {
                                isUserCreated = false;
                            }
                            else
                            {
                                isUserCreated = true;
                            }
                        }
                        else
                        {
                            isUserCreated = true;
                        }
                        //For setting password hash
                        if (isUserCreated)
                        {
                            PasswordHashDetail userDetailsForPasswordHash = new PasswordHashDetail()
                            {
                                CustomerShortCode = data.CustomerShortCode,
                                ApiUserName = data.ApiUserName,
                                ApiPassWord = data.ApiPassWord,
                                Upn = data.Upn,
                                UserSid = data.UserSid,
                                PasswordHash = userData.PasswordHash
                            };
                            var passwordHashContent = new JavaScriptSerializer().Serialize(userDetailsForPasswordHash);

                            if (!string.IsNullOrEmpty(passwordHashContent))
                            {
                                GetAndCheckResponse(GetCloudRequest("POST", "application/json", "/api/User/setpasswordhash", passwordHashContent));
                            }
                        }
                        return isUserCreated;
                    }
                    return false;
                }
                else
                {
                    new ExceptionHandler("Either UPN or Mail is null");
                    return false;
                }
            }
            catch (WebException webEx)
            {
                GetJSONExceptionMessage(webEx);
            }
            catch (Exception ex)
            {
                new ExceptionHandler(ex);
            }

            return false;
        }

        /// <summary>
        /// This method is used to delete the user from cloud
        /// </summary>
        /// <param name="userData">user details to delete</param>
        /// <returns>bool</returns>
        public bool DeleteUserFromCloud(UserDetail userData)
        {
            var content = new JavaScriptSerializer().Serialize(userData);
            try
            {
                if (GetAndCheckResponse(GetCloudRequest("POST", "application/json", "/api/User/delete", content)) == "failed")
                {
                    if (GetAndCheckResponse(GetCloudRequest("POST", "application/json", "/api/User/delete", content)) == "failed")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (WebException ex)
            {
                GetJSONExceptionMessage(ex);
            }
            catch (Exception ex)
            {
                new ExceptionHandler(ex);
            }
            return false;
        }

        /// <summary>
        /// This method is used to get the response of the request and also check the status of the request
        /// </summary>
        /// <param name="request">http request</param>
        /// <returns>string</returns>
        public string GetAndCheckResponse(HttpWebRequest request)
        {
            try
            {
                string str = GetResponseJsonBody(request);
                if (!string.IsNullOrEmpty(str))
                {
                    var jsonResponse = Json.Decode(str);
                    if (!string.IsNullOrEmpty(jsonResponse.ResponseId))
                        return CheckRequestStatus(jsonResponse.ResponseId);
                }
            }
            catch (WebException ex)
            {
                GetJSONExceptionMessage(ex);
            }
            catch (Exception ex)
            {
                new ExceptionHandler(ex);
            }
            finally
            {
                if (request != null)
                    request.Abort();
            }
            return "";
        }

        /// <summary>
        /// This method is used to check the status of the request
        /// </summary>
        /// <param name="requestId">request id</param>
        /// <returns>status of the request</returns>
        public string CheckRequestStatus(string requestId)
        {
            string requestStatus = string.Empty;
            try
            {
                RequestStatusData statusData = new RequestStatusData
                {
                    ApiUserName = ConfigurationManager.AppSettings["UserName"],
                    ApiPassWord = ConfigurationManager.AppSettings["Password"],
                    RequestId = requestId
                };
                var content = new JavaScriptSerializer().Serialize(statusData);
                while (true)
                {
                    string jsonBody = GetResponseJsonBody(GetCloudRequest("POST", "application/json", "/api/Customer/requeststatus", content));
                    if (string.IsNullOrEmpty(jsonBody))
                        break;
                    var result = Json.Decode(jsonBody);
                    if (result.RequestStatus != null && ((string)result.RequestStatus).ToLower() == "inprogress")
                    {
                        System.Threading.Thread.Sleep(50);
                        continue;
                    }
                    else
                    {
                        requestStatus = ((string)result.RequestStatus).ToLower();
                        break;
                    }
                }
                return requestStatus;
            }
            catch (WebException webEx)
            {
                GetJSONExceptionMessage(webEx);
            }
            catch (Exception ex)
            {
                new ExceptionHandler(ex);
            }
            return "";
        }

        /// <summary>
        /// This method is used to get the release version of the app
        /// </summary>
        /// <returns>Version details of the app</returns>
        public VersionDetails GetReleaseVersion()
        {
            try
            {
                string jsonBody = GetResponseJsonBody(GetCloudRequest("GET", "application/json", "/api/Software/version", ""));
                if (!string.IsNullOrEmpty(jsonBody))
                    return Json.Decode<VersionDetails>(jsonBody);
            }
            catch (WebException webEx)
            {
                GetJSONExceptionMessage(webEx);
            }
            catch (Exception ex)
            {
                new ExceptionHandler(ex);
            }
            return null;
        }

        /// <summary>
        /// This method is used to get the Json response body
        /// </summary>
        /// <param name="request">http request</param>
        /// <returns>json body</returns>
        private string GetResponseJsonBody(HttpWebRequest request)
        {
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();

                var encoding = ASCIIEncoding.ASCII;

                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException webEx)
            {
                GetJSONExceptionMessage(webEx);
            }
            catch (Exception ex)
            {
                new ExceptionHandler(ex);
            }
            finally
            {
                if (response != null)
                    response.Close();
                if (request != null)
                    request.Abort();
            }
            return "";
        }

        /// <summary>
        /// This method is used to get the exception message
        /// </summary>
        /// <param name="wex">web exception details</param>
        private void GetJSONExceptionMessage(WebException wex)
        {
            try
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            var result = Json.Decode(reader.ReadToEnd());
                            new ExceptionHandler(new Exception(result.Message));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ExceptionHandler(ex);
            }
        }

        /// <summary>
        /// This method is used for notifying the user about the new version of the application
        /// </summary>
        /// <param name="notification">notification details</param>
        public void NotifyCustomer(EmailNotification notification)
        {
            if (notification != null)
            {
                var content = new JavaScriptSerializer().Serialize(notification);
                if (!string.IsNullOrEmpty(content))
                {
                    try
                    {
                        var jsonMessage = GetResponseJsonBody(GetCloudRequest("POST", "application/json", "/api/Software/notify", content));
                    }
                    catch (WebException webEx)
                    {
                        GetJSONExceptionMessage(webEx);
                    }
                    catch (Exception ex)
                    {
                        new ExceptionHandler(ex);
                    }
                }
            }
        }

        /// <summary>
        /// This method is used to get the metadata properties for the ad users.
        /// </summary>
        /// <param name="cloudUser">AD user object</param>
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
            data.Telephone = string.IsNullOrEmpty(cloudUser.PhoneNumber) ? "" : cloudUser.PhoneNumber;
            data.Zip = string.IsNullOrEmpty(cloudUser.ZipCode) ? "" : cloudUser.ZipCode;
            data.City = string.IsNullOrEmpty(cloudUser.City) ? "" : cloudUser.City;
            data.Street = string.IsNullOrEmpty(cloudUser.Street) ? "" : cloudUser.Street;
            return data;
        }
    }
}
