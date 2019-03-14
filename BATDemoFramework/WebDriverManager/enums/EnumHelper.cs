using System;
using BATDemoFramework.NeyberPages.Profile;
using static BATDemoFramework.NeyberPages.Profile.AboutMePage;

namespace BATDemoFramework.WebDriverManager.enums
{
    public static class EnumHelper
    {
        public static RunEnv GetRunEnv(string value)
        {
            return (RunEnv)Enum.Parse(typeof(RunEnv), value.ToUpper(), ignoreCase: true);
        }

        public static TitleType GetTitleType(string value)
        {
            return (TitleType)Enum.Parse(typeof(TitleType), value, ignoreCase: true);
        }

        //public static MonthType GetMonthType(string value)
        //{
        //    return (MonthType)Enum.Parse(typeof(MonthType), value, ignoreCase: true);
        //}
    }
}
