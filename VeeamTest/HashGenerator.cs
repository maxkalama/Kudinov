using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace VeeamTest
{
    interface IHashGenerator
    {
        void GetHashForBlock(long blockNumber, Thread? waitForThread, ref ConcurrentDictionary<long, byte[]> results, ref byte[] buffer);
    }

    class HashGenerator : IHashGenerator
    {
        public void GetHashForBlock(long blockNumber, Thread? waitForThread, ref ConcurrentDictionary<long, byte[]> results, ref byte[] buffer)
        {
            var hash = SHA256.HashData(buffer);
            results[blockNumber] = hash;
            
            waitForThread?.Join();

            Console.WriteLine($"[{blockNumber}]{Convert.ToHexString(hash)}");
        }
    }
}
