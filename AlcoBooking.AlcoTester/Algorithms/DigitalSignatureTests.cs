using Xunit;
using AlcoBooking.Algorithms.RSA;
using AlcoBooking.Algorithms.DigitalSignature;

namespace AlcoBooking.Tests.Algorithms
{
    public class DigitalSignatureTests
    {
        private readonly RsaService _rsa = new();
        private readonly DigitalSignatureService _ds = new();

        [Fact]
        public void SignAndVerify_ShouldPass()
        {
            var keys = _rsa.GenerateKeys(512);
            string message = "Підписую важливе повідомлення";

            string signature = _ds.Sign(message, keys.PrivateKey);
            bool valid = _ds.Verify(message, signature, keys.PublicKey);

            Assert.True(valid);
        }

        [Fact]
        public void TamperedMessage_ShouldFail()
        {
            var keys = _rsa.GenerateKeys(512);
            string original = "Це важливо";
            string tampered = "Це не важливо";

            string signature = _ds.Sign(original, keys.PrivateKey);
            bool valid = _ds.Verify(tampered, signature, keys.PublicKey);

            Assert.False(valid);
        }

        [Fact]
        public void WrongKey_ShouldFail()
        {
            var keys1 = _rsa.GenerateKeys(512);
            var keys2 = _rsa.GenerateKeys(512);
            string msg = "Перевірка на підробку";

            string signature = _ds.Sign(msg, keys1.PrivateKey);
            bool valid = _ds.Verify(msg, signature, keys2.PublicKey);

            Assert.False(valid);
        }
    }
}
