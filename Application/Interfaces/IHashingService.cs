namespace Playmor_Asp.Application.Interfaces;

public interface IHashingService
{
    public (byte[], byte[]) CreateHash(string password);
    public bool CompareHash(string password, byte[] hash, byte[] salt);
}
