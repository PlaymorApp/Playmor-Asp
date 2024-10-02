using Playmor_Asp.Interfaces;
using System.Security.Cryptography;

namespace Playmor_Asp.Services;

public class HashingService : IHashingService
{
    public Tuple<byte[], byte[]> CreateHash(string password)
    {
        using (var hmac = new HMACSHA512())
        {
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return new Tuple<byte[], byte[]>(hash, salt);
        }
    }

    public bool CompareHash(string password, byte[] hash, byte[] salt)
    {
        using (var hmac = new HMACSHA512(salt))
        {
            var newHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return hash.SequenceEqual(newHash);
        }
    }
}
