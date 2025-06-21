using System.Numerics;

namespace AlcoBooking.Algorithms.RSA
{
    public class RsaKey
    {
        public BigInteger Exponent { get; set; }
        public BigInteger Modulus { get; set; }

        public RsaKey() { }

        public RsaKey(BigInteger exponent, BigInteger modulus)
        {
            Exponent = exponent;
            Modulus = modulus;
        }

        public BigInteger Apply(BigInteger input)
        {
            return BigInteger.ModPow(input, Exponent, Modulus);
        }
    }
}
