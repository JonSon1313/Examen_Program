using System.Security.Cryptography;
using System.Text;

namespace DataHashing
{
    public class Hashing
    {
        public static string ToHashSha256WithSalt(byte[] salt, string data)
        {
            byte[] saltedData = CombineSaltWithData(salt, data);
            byte[] dataHash = ComputeHash(saltedData);

            return Convert.ToBase64String(dataHash);
        }

        public static (string, string) ToHashSha256WithSalt(string data)
        {
            byte[] salt = GenerateSalt();
            byte[] saltedData = CombineSaltWithData(salt, data);

            byte[] dataHash = ComputeHash(saltedData);

            string hashedData = Convert.ToBase64String(dataHash);
            string hashedSalt = Convert.ToBase64String(salt);
            return (hashedData, hashedSalt);
        }

        private static byte[] GenerateSalt()
        {
            using var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }

        private static byte[] CombineSaltWithData(byte[] salt, string data)
        {
            byte[] databytes = Encoding.UTF8.GetBytes(data);
            byte[] saltedData = new byte[salt.Length + databytes.Length];

            Buffer.BlockCopy(salt, 0, saltedData, 0, salt.Length);
            Buffer.BlockCopy(databytes, 0, saltedData, 0, databytes.Length);

            return saltedData;
        }

        //Currently unused
        /*
        private static byte[] CombineSaltWithData(byte[] salt, byte[] data)
        {
            byte[] saltedData = new byte[salt.Length + data.Length];

            Buffer.BlockCopy(salt, 0, saltedData, 0, salt.Length);
            Buffer.BlockCopy(data, 0, saltedData, 0, data.Length);

            return saltedData;
        }
        */
        //

        private static byte[] ComputeHash(byte[] data)
        {
            return SHA256.HashData(data);
        }
    }
}
