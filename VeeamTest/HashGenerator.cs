using System.Security.Cryptography;

namespace VeeamTest
{
    interface IHashGenerator
    {
        void GetHashForBlock(long blockNumber, Thread? waitForThread, ref byte[] buffer, ref int threadCount);
    }

    class HashGenerator : IHashGenerator
    {
        public void GetHashForBlock(long blockNumber, Thread? waitForThread, ref byte[] buffer, ref int threadCount)
        {
            Interlocked.Increment(ref threadCount);
            Console.WriteLine($"[{blockNumber}] is working. threadCount {threadCount}.");
            var hash = SHA256.HashData(buffer);
            Thread.Sleep(1000);
            waitForThread?.Join();
            Interlocked.Decrement(ref threadCount);

            Console.WriteLine($"[{blockNumber}]{Convert.ToHexString(hash)}. threadCount {threadCount}.");
        }
    }
}
