using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.IO;

namespace UserSyncClient
{
    public class ApiUserCredential
    {
        public string ApiUserName { get; set; }

        public string ApiPassWord { get; set; }
    }

    public class UserDetail : ApiUserCredential
    {
        public string Upn { get; set; }

        public string UserSid { get; set; }

        public string CustomerShortCode { get; set; }

    }

    public class PasswordHashDetail : UserDetail
    {
        public string PasswordHash { get; set; }
    }

    public class CloudData : UserDetail
    {

        public string SamAccountName { get; set; }

        //public string PassWord { get; set; }        

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public EmailDetail[] EmailAddresses { get; set; }

        //public string Address { get; set; }

        public string Telephone { get; set; }

        //public string MobileNumber { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
    }

    public class EmailDetail
    {
        public string Email { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class VersionDetails
    {
        public string LatestVersion { get; set; }

        public string ReleaseDate { get; set; }
    }

    public class EmailNotification
    {
        public string CustomerShortCode { get; set; }

        public string NotificationEmail { get; set; }

        public string Release { get; set; }
    }

    public class ExceptionMessageData
    {
        private string _message;
        public string Message { get { return _message; } set { _message = value; } }
    }

    public class RequestStatusData : ApiUserCredential
    {
        public string RequestId { get; set; }
    }
}