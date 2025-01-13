using System.Text;

namespace LinkApp.API.Utils
{
    internal static class BaseEncode
    {
        private static readonly char[] _alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz".ToCharArray();

        public static int ENCODE => _alphabet.Length;

        public static byte[] Encode(string link)
        {
            var hash = Math.Abs(link.GetHashCode());
            var bytes = new List<byte>();
            var i = 0;

            while (hash > 0)
            {
                var remainder = hash % ENCODE;

                bytes.Add((byte)remainder);
                hash /= ENCODE;
                i++;
            }

            return [.. bytes];
        }

        public static string Decode(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (var @byte in bytes)
            {
                sb.Append(_alphabet[@byte]);
            }

            return sb.ToString();
        }
    }
}
