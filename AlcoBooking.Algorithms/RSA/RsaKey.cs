using System.Numerics;

namespace AlcoBooking.Algoritms.RSA
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
    }
}
