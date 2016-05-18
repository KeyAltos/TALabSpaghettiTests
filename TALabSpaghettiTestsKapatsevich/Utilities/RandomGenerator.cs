using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALabSpaghettiTestsKapatsevich.TestsConstants;

namespace TALabSpaghettiTestsKapatsevich.Utilities
{
    public static class RandomGenerator
    {
        public static string GetRandomString(int length)
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            Random random = new Random((int)DateTime.Now.Ticks);
            string result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }

        public static string GetRandomMessageTitle(string firstWord)
        {
            return firstWord + " message title " + GetRandomString(3);
        }


        public static Message GetRandomMessage(string keyWord)
        {
            return new Message()
            {
                Title = GetRandomMessageTitle(keyWord),
                Text = keyWord + @" message text."
            };
        }
    }
}
