using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATDemoFramework.config
{
    class TestConfigurationManager
    {
        private static TestConfigurationManager instance = null;

        public const string TEST_BROWSER = "testBrowser";
        public const string TEST_ENVIRONMENT = "testEnvironment";
        public const string RUN_ON = "runOn";

        private TestConfigurationManager()
        {
        }

        public static TestConfigurationManager GetInstance()
        {
            if (instance == null)
                return new TestConfigurationManager();

            return instance;
        }

        public string GetTestBrowser()
        {
            return GetEnvironmentVariable(TEST_BROWSER, "Chrome");
        }

        public string GetTestEnv()
        {
            return GetEnvironmentVariable(TEST_ENVIRONMENT, "UAT");
        }

        public string GetRunOn()
        {
            return GetEnvironmentVariable(RUN_ON, "LOCAL");
        }

        private string GetEnvironmentVariable(string variable, string defaultValue)
        {
            return Environment.GetEnvironmentVariable(variable) != null ? System.Environment.GetEnvironmentVariable(variable) : defaultValue;
        }
    }
}