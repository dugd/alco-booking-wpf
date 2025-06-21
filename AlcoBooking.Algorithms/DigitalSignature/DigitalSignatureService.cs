using AlcoBooking.Algorithms.RSA;
using System.Collections;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace AlcoBooking.Algorithms.DigitalSignature
{
    public class DigitalSignatureService
    {
        public string Sign(string message, RsaKey privateKey)
        {
            byte[] hash = ComputeHash(message);
            BigInteger hashInt = new BigInteger(hash, isUnsigned: true, isBigEndian: true);
            BigInteger signature = privateKey.Apply(hashInt);
            byte[] signatureBytes = signature.ToByteArray(isUnsigned: true, isBigEndian: true);
            return Convert.ToBase64String(signatureBytes);
        }

        public bool Verify(string message, string base64Signature, RsaKey publicKey)
        {
            byte[] signatureBytes = Convert.FromBase64String(base64Signature);
            BigInteger signature = new BigInteger(signatureBytes, isUnsigned: true, isBigEndian: true);
            BigInteger hashInt = publicKey.Apply(signature);
            byte[] hashBytes = hashInt.ToByteArray(isUnsigned: true, isBigEndian: true);

            byte[] actualHash = ComputeHash(message);
            return StructuralComparisons.StructuralEqualityComparer.Equals(TrimLeadingZeros(hashBytes), actualHash);
        }

        private static byte[] ComputeHash(string message)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(Encoding.UTF8.GetBytes(message));
        }

        private static byte[] TrimLeadingZeros(byte[] bytes)
        {
            int i = 0;
            while (i < bytes.Length && bytes[i] == 0)
                i++;
            byte[] trimmed = new byte[bytes.Length - i];
            Array.Copy(bytes, i, trimmed, 0, trimmed.Length);
            return trimmed;
        }
    }
}
