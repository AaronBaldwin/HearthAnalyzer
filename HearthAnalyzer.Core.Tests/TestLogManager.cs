using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.Simple;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    public static class TestLogManager
    {
        private static ILog _logger;

        private static bool _initialized = false;

        public static string TestOuputFileName = "TestDebugOuput.txt";

        /// <summary>
        /// The logger instance
        /// </summary>
        public static ILog Logger
        {
            get
            {
                if (_logger == null)
                {
                    var loggingProperties = new NameValueCollection();
                    loggingProperties["showDateTime"] = "true";
                    LogManager.Adapter = new TraceLoggerFactoryAdapter(loggingProperties);
                    Trace.Listeners.Add(new TextWriterTraceListener(TestOuputFileName));
                    Trace.AutoFlush = true;

                    _logger = LogManager.GetCurrentClassLogger();
                }

                return _logger;
            }
        }

        /// <summary>
        /// Initialize the HearthAnalyzer.Core logger
        /// </summary>
        /// <param name="context">The test context</param>
        public static void Initialize(TestContext context)
        {
            if (!_initialized)
            {
                if (File.Exists(TestLogManager.TestOuputFileName))
                {
                    File.Delete(TestLogManager.TestOuputFileName);
                }

                TestLogManager.TestOuputFileName = Path.Combine(context.TestRunDirectory, "TestDebugOutput.txt");
                HearthAnalyzer.Core.Logger.InitializeLogger(TestLogManager.Logger);
                context.AddResultFile(TestOuputFileName);

                _initialized = true;
            }
        }
    }
}
