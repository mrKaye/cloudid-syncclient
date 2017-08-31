using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSyncClient
{
    public class SecurityGroupUser
    {
        public string Sid { get; set; }

        public string Upn { get; set; }

        public DateTime TimeStamp { get; set; }

        public string SamAccountName { get; set; }

        public string Description { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public EmailDetail[] EmailAddresses { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        //public string Password { get; set; }

        public string PasswordHash { get; set; }
    }
}
