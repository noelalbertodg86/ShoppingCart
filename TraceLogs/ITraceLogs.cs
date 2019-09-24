using System;
using System.Collections.Generic;
using System.Text;

namespace TraceLogs
{
    public interface ITraceLogs
    {
        void SaveInformationLogs(string logMessage);
        void SaveWarningLogs(string logMessage);
        void SaveErrorLogs(Exception _e);
    }
}
