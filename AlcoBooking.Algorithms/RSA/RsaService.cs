using System.Numerics;
using System.Text;

namespace AlcoBooking.Algorithms.RSA
{
    public class RsaService
    {
       public RsaKeyPair GenerateKeys(int bitLength)
        {
            BigInteger P = PrimeGenerator.GeneratePrime(bitLength / 2);
            BigInteger Q = PrimeGenerator.GeneratePrime(bitLength / 2);
            BigInteger N = P * Q;
            BigInteger Phi = (P - 1) * (Q - 1);

            BigInteger e = 65537;
            while (GCD(e, Phi) != 1)
                e += 2;

            BigInteger d = ModInverse(e, Phi);

            RsaKey PublicKey = new RsaKey(e, N);
            RsaKey PrivateKey = new RsaKey(d, N);

            return new RsaKeyPair(PublicKey, PrivateKey);
        }

        private static BigInteger GCD(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                BigInteger t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        private static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m, y = 0, x = 1;
            while (a > 1)
            {
                BigInteger q = a / m;
                (a, m) = (m, a % m);
                (x, y) = (y, x - q * y);
            }
            return x < 0 ? x + m0 : x;
        }

        public string Encrypt(string message, RsaKey publicKey)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            BigInteger m = new BigInteger(messageBytes, isUnsigned: true, isBigEndian: true);

            if (m >= publicKey.Modulus)
                throw new ArgumentException("Message too long for this key.");

            BigInteger c = publicKey.Apply(m);
            byte[] encryptedBytes = c.ToByteArray(isUnsigned: true, isBigEndian: true);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string Decrypt(string encrypted, RsaKey privateKey)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encrypted);
            BigInteger c = new BigInteger(encryptedBytes, isUnsigned: true, isBigEndian: true);

            BigInteger m = privateKey.Apply(c);
            byte[] messageBytes = m.ToByteArray(isUnsigned: true, isBigEndian: true);
            return Encoding.UTF8.GetString(messageBytes);
        }
    }
}
