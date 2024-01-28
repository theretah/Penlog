using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace Penlog.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string ShortenString(this string input, int count)
        {
            var separated = input.Split(' ');
            int limit = separated.Length > count ? count : separated.Length;
            var builder = new StringBuilder();

            for (int i = 0; i < limit; i++)
                builder.Append(separated[i] + " ");

            var result = builder.ToString().StripString();

            return count == limit ? result + "..." : result;
        }

        private static string StripString(this string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}