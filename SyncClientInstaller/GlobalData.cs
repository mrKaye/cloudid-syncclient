using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncClientInstaller
{
    public static class GlobalData
    {
        private static Form _frmName;
        public static Form FrmName
        {
            get { return _frmName; }
            set { _frmName = value; }
        }

        private static string _installationPath;
        public static string InstallationPath
        {
            get { return _installationPath; }
            set { _installationPath = value; }
        }

        private static Dictionary<string, string> _appSettingsData;
        public static Dictionary<string, string> AppSettingsData
        {
            get { return _appSettingsData; }
            set { _appSettingsData = value; }
        }

        private static int _scheduleTime;
        public static int ScheduleTime
        {
            get { return _scheduleTime; }
            set { _scheduleTime = value; }
        }

        private static string _language;
        public static string Language
        {
            get { return _language; }
            set { _language = value; }
        }

        private static bool _isBtnClose = false;
        public static bool IsBtnClose
        {
            get { return _isBtnClose; }
            set { _isBtnClose = value; }
        }

        private static string _backFormName;
        public static string BackFormName
        {
            get { return _backFormName; }
            set { _backFormName = value; }
        }

        private static string _nextFormName;
        public static string NextFormName
        {
            get { return _nextFormName; }
            set { _nextFormName = value; }
        }

        private static int _linkNumber;
        public static int LinkNumber
        {
            get { return _linkNumber; }
            set { _linkNumber = value; }
        }

        private static bool _isInstallationSuccess;
        public static bool IsInstallationSuccess
        {
            get { return _isInstallationSuccess; }
            set { _isInstallationSuccess = value; }
        }

        private static string _ouPath;
        public static string OUPath
        {
            get { return _ouPath; }
            set { _ouPath = value; }
        }

        private static bool _isCheckBoxChecked;
        public static bool IsCheckBoxChecked
        {
            get { return _isCheckBoxChecked; }
            set { _isCheckBoxChecked = value; }
        }

        private static string _ldapString;
        public static string LdapString
        {
            get { return _ldapString; }
            set { _ldapString = value; }
        }

        private static string _ouString;
        public static string OUString
        {
            get { return _ouString; }
            set { _ouString = value; }
        }
    }
}
