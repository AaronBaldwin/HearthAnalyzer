using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common.Logging;

namespace HearthAnalyzer.Core
{
    public static class Logger
    {
        private static ILog log;

        /// <summary>
        /// Initializes the logger
        /// </summary>
        /// <param name="logger">Optional logger instance to use</param>
        public static void InitializeLogger(ILog logger = null)
        {
            if (logger != null)
            {
                log = logger;
            }
            else
            {
                log = LogManager.GetCurrentClassLogger();
            }
        }

        /// <summary>
        /// Returns the instance of the logger
        /// </summary>
        public static ILog Instance
        {
            get
            {
                if (log == null)
                {
                    InitializeLogger();
                }

                return log;
            }
        }
    }
}
