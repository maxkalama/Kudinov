using System.Security.Cryptography;

namespace VeeamTest
{
    interface IHashGenerator
    {
        void GetHashForBlock(long blockNumber, Task? waitTask, ref byte[] buffer, ref int taskCount);
    }

    class HashGenerator : IHashGenerator
    {
        public void GetHashForBlock(long blockNumber, Task? waitTask, ref byte[] buffer, ref int taskCount)
        {
            Interlocked.Increment(ref taskCount);
            Console.WriteLine($"[{blockNumber}] is working. taskCount {taskCount}.");
            var hash = SHA256.HashData(buffer);
            Thread.Sleep(1000);
            waitTask?.Wait();
            Interlocked.Decrement(ref taskCount);

            Console.WriteLine($"[{blockNumber}]{Convert.ToHexString(hash)}. taskCount {taskCount}.");
        }
    }
}
