using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iFinance.Models
{
    public static class Global
    {
        public static List<string> DefaultTag = new List<string>()
        {
            "吃饭","旅行", "买书","娱乐","演出","交通"
        };
        public static long getTimestamp(DateTime time)
        {
            TimeSpan ts = new TimeSpan(time.Ticks - new DateTime(1970, 1, 1).Ticks);
            return (long)ts.TotalMilliseconds;
        }
        public static void addDefaultTag(int uid)
        {
            Provider db = new Provider();
            foreach (string t in DefaultTag)
            {
                Tag tag = new Tag()
                {
                    Name = t,
                    UserId = uid,
                    Count = 0
                };
                db.insertTag(tag);
            }
        }

        public static string md5(string s)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5").ToLower();
        }
    }
}