using System;

namespace MediaInventory.Infrastructure.Common
{
    public static class DateTimeExtensions
    {
        public static DateTime ParseMicrosoftJsonDateFormat(this string date)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(long.Parse(date.Replace("/Date(", "").Replace(")/", ""))).ToLocalTime();
        }
    }
}