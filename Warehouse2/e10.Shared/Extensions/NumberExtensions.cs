using System;
using System.Security.Cryptography;
using System.Text;

namespace e10.Shared.Extensions
{
    public static class NumberExtensions
    {
        private static readonly char[] Jokers =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V','W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v','w', 'x', 'y', 'z'
        };

        private static readonly Base10Converter Convertror = new Base10Converter(Jokers);

        public static Name ToName(this string value)
        {
            if(string.IsNullOrWhiteSpace(value)) return new Name();
            var names= value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new Name
            {
                First = names[0],
                Last = names.Length > 1 ? names[names.Length - 1] : ""
            };
        }
        public static long ToBase10(this string hexValue)
        {
            return Convertror.StringToBase10(hexValue);
        }

        public static string ToGravtaar(this string email)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string Base10ToString(this long number)
        {
            return Convertror.Base10ToString(number);
        }



        public static string Base10ToString(this int number)
        {
            return Convertror.Base10ToString(number);
        }

    }
}