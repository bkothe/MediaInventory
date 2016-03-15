using System;

namespace MediaInventory.Infrastructure.Common
{
    public static class StringExtensions
    {
        public static string ToFormat(this string content, params object[] replacements)
        {
            return string.Format(content, replacements);
        }

        public class ParseResult<T> { public T Value; }

        public static bool TryParse<T>(Func<ParseResult<T>, bool> parse, out object result)
        {
            var value = new ParseResult<T>();
            var success = parse(value);
            result = value.Value;
            return success;
        }

        public static T Parse<T>(this string value)
        {
            object result;
            return value.TryParse(typeof(T), out result) ? (T)result : default(T);
        }

        public static bool TryParse(this string value, Type type, out object parseResult)
        {
            parseResult = null;

            if (value == null) return false;

            if (type == typeof(string))
            {
                parseResult = value;
                return true;
            }

            if (type == typeof(bool)) return TryParse<bool>(v => value.TryParseBool(out v.Value), out parseResult);
            if (type == typeof(byte)) return TryParse<byte>(v => byte.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(char)) return TryParse<char>(v => char.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(DateTime)) return TryParse<DateTime>(v => DateTime.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(decimal)) return TryParse<decimal>(v => decimal.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(double)) return TryParse<double>(v => double.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(short)) return TryParse<short>(v => short.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(int)) return TryParse<int>(v => int.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(long)) return TryParse<long>(v => long.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(sbyte)) return TryParse<sbyte>(v => sbyte.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(float)) return TryParse<float>(v => float.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(ushort)) return TryParse<ushort>(v => ushort.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(uint)) return TryParse<uint>(v => uint.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(ulong)) return TryParse<ulong>(v => ulong.TryParse(value, out v.Value), out parseResult);
            if (type == typeof(Guid)) return TryParse<Guid>(v => TryParseGuid(value, out v.Value), out parseResult);
            if (type == typeof(TimeSpan)) return TryParse<TimeSpan>(v => TimeSpan.TryParse(value, out v.Value), out parseResult);

            throw new NotSupportedException($"Parsing of type {type.Name} is not suported.");
        }

        public static bool TryParseGuid(this string value, out Guid result)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = Guid.Empty;
                return false;
            }
            try
            {
                result = new Guid(value);
                return true;
            }
            catch
            {
                result = Guid.Empty;
                return false;
            }
        }

        public static bool TryParseBool(this string value, out bool result)
        {
            bool parsedResult;
            if (bool.TryParse(value, out parsedResult))
            {
                result = parsedResult;
                return true;
            }
            if ("yes".Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
                return true;
            }
            if ("no".Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = false;
                return true;
            }
            result = false;
            return false;
        }
        public static string[] Split(this string value, string splitOn)
        {
            return value.Split(new[] { splitOn }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}