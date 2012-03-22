using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace OfflineSiteMonitoringTool.Model
{
    public class Logger : ILogger
    {
        private List<String> log = new List<string>();
        private List<string> trace = new List<string>();

        public void Add(string message)
        {
            // Taken from http://heifner.blogspot.com/2006/12/logging-method-name-in-c.html
            // Start one up so that we don't get the current method but the one that called this one
            StackFrame sf = new StackFrame(1, true);
            System.Reflection.MethodBase mb = sf.GetMethod();
            string methodName = mb != null ? mb.Name : "ERROR DETERMINING CALLING METHOD NAME";
            // note to self: not sure if i need the data captured below this point
            // filename can be null, if unable to determine
            string filename = sf.GetFileName();
            // we only want the filename, not the complete path
            if (filename != null)
                filename = filename.Substring(filename.LastIndexOf('\\') + 1);

            string logMessage =  message;
            string traceMessage = DateTime.Now.ToString() + "  :  " + filename + "  :  " + methodName + "  :  " + message;

            log.Add(logMessage);
            trace.Add(traceMessage);
        }

        public void Write()
        {
            WriteToLogFile();
        }

        private void WriteToLogFile()
        {
            StreamWriter stream = new StreamWriter("_log.txt");

            foreach (string _logEntry in log)
            {
                stream.WriteLine(_logEntry.ToString());
            }
            stream.Close();

            stream = new StreamWriter("_trace.txt");

            foreach (string traceEntry in trace)
                stream.WriteLine(traceEntry);

            stream.Close();
        }
    }
}
