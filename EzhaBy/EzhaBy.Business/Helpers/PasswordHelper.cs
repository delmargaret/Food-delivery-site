using System;
using System.Linq;

namespace EzhaBy.Business.Helpers
{
    public static class PasswordHelper
    {
        private static readonly Random random = new Random();
        public static string RandomPassword(int length)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789-_";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
