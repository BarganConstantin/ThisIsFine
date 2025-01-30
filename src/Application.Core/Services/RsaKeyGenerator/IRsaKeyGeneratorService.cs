namespace Application.Core.Services.RsaKeyGenerator
{
    public interface IRsaKeyGeneratorService
    {
        void GenerateAndSavePrivateKey(string filePath);
    }
}
