using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace VeeamTest
{
    interface IHashGenerator
    {
        void GetHashForBlock(long blockNumber, ref ConcurrentDictionary<long, int> results, ref byte[] buffer);
    }

    class HashGenerator : IHashGenerator
    {
        public void GetHashForBlock(long blockNumber, ref ConcurrentDictionary<long, int> results, ref byte[] buffer)
        {
            var hash = SHA256.(buffer);
            results[blockNumber] = hash;
        }
    }
}
