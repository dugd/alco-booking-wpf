namespace AlcoBooking.Algorithms.RSA
{
    public class RsaKeyPair
    {
        public RsaKey PublicKey { get; }
        public RsaKey PrivateKey { get; }

        public RsaKeyPair(RsaKey publicKey, RsaKey privateKey)
        {
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;
        }
    }
}
