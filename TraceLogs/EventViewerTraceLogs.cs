using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TraceLogs
{
    public class EventViewerTraceLogs: ITraceLogs
    {
        private string source;

        public EventViewerTraceLogs(string _source)
        {
            this.source = _source;
        }
        public void SaveInformationLogs(string logMessage)
        {
            System.Diagnostics.EventLog.WriteEntry(this.source, logMessage, EventLogEntryType.Information);
        }

        public void SaveWarningLogs(string logMessage)
        {
            System.Diagnostics.EventLog.WriteEntry(this.source, logMessage, EventLogEntryType.Warning);
        }

        public void SaveErrorLogs(Exception _e)
        {
            string logMessage = $" Error: {_e.Message} InnerException: {_e.InnerException} TraceBack: {_e.StackTrace}";
            System.Diagnostics.EventLog.WriteEntry(this.source, logMessage, EventLogEntryType.Error);
        }
    }
}
