using AlcoBooking.Algorithms.RSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoBooking.AlcoTester.Algorithms
{
    public class RSATest
    {
        private RsaService RSA => new RsaService();

        [Fact]
        public void EncryptDecrypt_ShouldReturnOriginalMessage()
        {
            var keyPair = RSA.GenerateKeys(512);
            string originalMessage = "Hello, RSA!";

            string encrypted = RSA.Encrypt(originalMessage, keyPair.PublicKey);
            string decrypted = RSA.Decrypt(encrypted, keyPair.PrivateKey);

            Assert.Equal(originalMessage, decrypted);
        }

        [Fact]
        public void Encrypt_ThrowsIfMessageTooLong()
        {
            var keyPair = RSA.GenerateKeys(128); // smol N
            string message = new string('A', 100);

            Assert.Throws<ArgumentException>(() =>
            {
                RSA.Encrypt(message, keyPair.PublicKey);
            });
        }

        [Fact]
        public void Decrypt_ReturnsCorrectlyEvenWithSpecialCharacters()
        {
            var keyPair = RSA.GenerateKeys(512);
            string message = "Привіт!";

            string encrypted = RSA.Encrypt(message, keyPair.PublicKey);
            string decrypted = RSA.Decrypt(encrypted, keyPair.PrivateKey);

            Assert.Equal(message, decrypted);
        }
    }
}
