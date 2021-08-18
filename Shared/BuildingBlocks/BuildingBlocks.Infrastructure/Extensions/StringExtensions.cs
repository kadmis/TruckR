using System;
using System.Text;

namespace BuildingBlocks.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64(this string @string)
        {
            try
            {
                var bytes = Encoding.ASCII.GetBytes(@string);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                return @string;
            }
        }

        public static string FromBase64(this string @string)
        {
            try
            {
                var bytes = Convert.FromBase64String(@string);
                return Encoding.ASCII.GetString(bytes);
            }
            catch (Exception)
            {
                return @string;
            }
        }
    }
}
