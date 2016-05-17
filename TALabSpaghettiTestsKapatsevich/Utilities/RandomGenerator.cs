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
            Random random = new Random((int)DateTime.Now.Ticks);
            return firstWord + GetRandomString(random.Next(1,3)) +" message"+GetRandomString(random.Next(1,3)) + " title" + GetRandomString(random.Next(1,3));
        }


        public static Message GetRandomMessage(string keyWord)
        {
            return new Message()
            {
                Title = GetRandomMessageTitle(keyWord),
                Text = keyWord + @" message text.
                Mauris ante tellus, posuere quis laoreet elementum, aliquet vel nisl. Sed id erat euismod, tempor lorem et, tincidunt justo. Duis quis lacinia libero, vitae laoreet tellus. Phasellus malesuada egestas dui, eu commodo nunc finibus at. In interdum in nibh viverra pretium. Nunc at nibh in enim ornare accumsan. Donec in lorem nec erat accumsan maximus quis at metus. Suspendisse aliquet fermentum tincidunt."
            };
        }
    }
}
