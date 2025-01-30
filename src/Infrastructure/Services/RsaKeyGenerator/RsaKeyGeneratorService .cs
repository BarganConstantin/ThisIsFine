using System.Security.Cryptography;
using Application.Core.Services.RsaKeyGenerator;

namespace ThiIsFine.Infrastructure.Services.RsaKeyGenerator
{
    public class RsaKeyGeneratorService : IRsaKeyGeneratorService
    {
        public void GenerateAndSavePrivateKey(string filePath)
        {
            using (var rsa = RSA.Create())
            {
                var privateKeyBytes = rsa.ExportRSAPrivateKey();
                File.WriteAllBytes(filePath, privateKeyBytes);
            }
        }
    }
}
