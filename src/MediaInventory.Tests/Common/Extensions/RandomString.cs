using System;

namespace MediaInventory.Tests.Common.Extensions
{
    public static class RandomString
    {
        //public static string Generate(int length = 10)
        //{
        //    return Membership.GeneratePassword(length, 0);
        //}

        private static readonly Random Generator = new Random();

        public static string GenerateNumeric(int length = 10)
        {
            var result = "";
            while (true)
            {
                result += Generator.Next((int)Math.Pow(10, 8),
                    (int)Math.Pow(10, 9) - 1).ToString();
                if (result.Length > length) return result.Substring(0, length);
            }
        }

        public static string GenerateAlphaNumeric(int length = 10)
        {
            var result = "";
            while (true)
            {
                result += Guid.NewGuid().ToString("N");
                if (result.Length > length) return result.Substring(0, length);
            }
        }

        public static string GenerateEmail()
        {
            return $"{GenerateAlphaNumeric()}@{GenerateAlphaNumeric()}.com";
        }

        public static string GenerateIpAddress()
        {
            return $"{Generator.Next(1, 255)}.{Generator.Next(1, 255)}.{Generator.Next(1, 255)}.{Generator.Next(1, 255)}";
        }
    }
}