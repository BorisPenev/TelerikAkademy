using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WcfDemo.Server
{
    public class DemoService : IDemoService
    {
        public string GetDayOfWeek(DateTime value)
        {
            var bulgarianCultureInfo = new CultureInfo("bg-BG");
            return DateTime.Now.ToString("dddd", bulgarianCultureInfo);
        }

        public int GetStringRepeatedCount(string text, string search)
        {
            int count = new Regex(search).Matches(text).Count;
            return count;
        }
    }
}
