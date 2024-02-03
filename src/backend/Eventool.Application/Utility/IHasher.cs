using System.Security.Cryptography;

namespace Eventool.Application.Utility;

public record Hashed(string Value, string Salt);

public interface IHasher
{
    Hashed Hash(string value, string? salt = null);
}

public class Sha512Hasher : IHasher
{
    private const int SaltSize = 16;
    private const int ValueSize = 32;

    public Hashed Hash(string value, string? salt = null)
    {
        var saltBytes = salt is null
            ? GenerateRandomSalt()
            : Convert.FromBase64String(salt);

        using var algorithm = new Rfc2898DeriveBytes(
            value,
            saltBytes,
            10000,
            HashAlgorithmName.SHA512);

        var base64Key = Convert.ToBase64String(algorithm.GetBytes(ValueSize));
        var base64Salt = Convert.ToBase64String(algorithm.Salt);

        return new Hashed(base64Key, base64Salt);
    }
    
    private static byte[] GenerateRandomSalt()
    {
        var bytes = new byte[SaltSize];
        Random.Shared.NextBytes(bytes);
        return bytes;
    }
}