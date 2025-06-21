using System.Numerics;

namespace AlcoBooking.Algorithms.RSA
{
    public static class PrimeGenerator
    {
        public static BigInteger GeneratePrime(int bits)
        {
            Random rnd = new();
            while (true)
            {
                byte[] bytes = new byte[bits / 8 + 1];
                rnd.NextBytes(bytes);
                bytes[^1] = 0;
                BigInteger candidate = new(bytes);
                if (candidate % 2 == 0) candidate += 1;
                if (IsPrime(candidate))
                    return candidate;
            }
        }

        private static BigInteger RandomBigInt(BigInteger min, BigInteger max, Random rnd)
        {
            byte[] bytes = max.ToByteArray();
            BigInteger result;
            do
            {
                rnd.NextBytes(bytes);
                bytes[^1] = 0;
                result = new BigInteger(bytes);
            } while (result < min || result >= max);
            return result;
        }

        private static bool IsPrime(BigInteger n, int k = 5)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0) return false;

            BigInteger d = n - 1;
            int s = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            Random rnd = new();
            for (int i = 0; i < k; i++)
            {
                BigInteger a = RandomBigInt(2, n - 2, rnd);
                BigInteger x = BigInteger.ModPow(a, d, n);
                if (x == 1 || x == n - 1) continue;

                bool continueOuter = false;
                for (int r = 0; r < s - 1; r++)
                {
                    x = BigInteger.ModPow(x, 2, n);
                    if (x == n - 1)
                    {
                        continueOuter = true;
                        break;
                    }
                }
                if (!continueOuter)
                    return false;
            }
            return true;
        }
    }
}
