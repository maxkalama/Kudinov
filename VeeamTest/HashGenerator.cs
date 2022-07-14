using System.Security.Cryptography;

namespace VeeamTest
{
    interface IHashGenerator
    {
        void GetHashForBlock(long blockNumber, Thread? waitForThread, ref byte[] buffer);
    }

    class HashGenerator : IHashGenerator
    {
        public void GetHashForBlock(long blockNumber, Thread? waitForThread, ref byte[] buffer)
        {
            Console.WriteLine($"[{blockNumber}] is working.");
            var hash = SHA256.HashData(buffer);
            waitForThread?.Join();

            Console.WriteLine($"[{blockNumber}]{Convert.ToHexString(hash)}");
        }
    }
}
