namespace Playmor_Asp.Interfaces
{
    public interface IHashingService
    {
        public Tuple<byte[], byte[]> CreateHash(string password);
        public bool CompareHash(string password, byte[] hash, byte[] salt);
    }
}
