namespace CleanArchitecture.Domain.Interfaces.Security
{
    public interface ICryptoUtils
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
