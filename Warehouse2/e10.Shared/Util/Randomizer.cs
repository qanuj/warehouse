using System;
using System.Text;

namespace e10.Shared.Util
{
    public class Randomizer
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);
        const string CharsSpecial = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz$#@!^*~`[]";
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        public static string Generate(int size, bool specials = true)
        {
            var text = specials ? CharsSpecial : Chars;
            var builder = new StringBuilder();
            var i = 0;
            while (i < size)
            {
                var at = Convert.ToInt32(Random.NextDouble() * text.Length);
                if (at >= text.Length) continue;
                builder.Append(text[at]);
                i++;
            }
            return builder.ToString();
        }
    }
}