using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSyncClient
{
    public class ExceptionHandler : Exception
    {
        public ExceptionHandler()
        { }

        private const String MachineName = ".";

        public ExceptionHandler(string message)
            : base(message)
        {
            if (!EventLog.SourceExists("Application", MachineName))
            {
                EventLog.CreateEventSource(new EventSourceCreationData("EVRY", "Application"));
            }
            EventLog eventLog = new EventLog { Source = "EVRY" };
            eventLog.WriteEntry("Message:" + message + Environment.NewLine, EventLogEntryType.Error);
        }

        public ExceptionHandler(Exception ex)
            : base(ex.Message, ex)
        {
            //sourcename:Application
            //ViewerName:EVRY
            if (!EventLog.SourceExists("Application", MachineName))
            {
                EventLog.CreateEventSource(new EventSourceCreationData("EVRY", "Application"));
            }
            EventLog eventLog = new EventLog { Source = "EVRY" };
            eventLog.WriteEntry("Message:" + ex.Message + Environment.NewLine + "Stack Trace:" + ex.StackTrace, EventLogEntryType.Error);
        }
    }
}
