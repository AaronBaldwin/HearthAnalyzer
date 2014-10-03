using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthAnalyzer.Core.Tests
{
    [TestClass]
    public class BaseTestSuite
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            TestLogManager.Initialize(context);
        }
    }
}
