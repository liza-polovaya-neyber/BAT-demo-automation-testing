using System;

namespace BATDemoFramework.config
{
    public class TestConfigurationManager
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
            return instance ?? new TestConfigurationManager();
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
            return Environment.GetEnvironmentVariable(variable) != null ? Environment.GetEnvironmentVariable(variable) : defaultValue;
        }
    }
}