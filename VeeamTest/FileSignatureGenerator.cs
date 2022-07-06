using System.Collections.Concurrent;

namespace VeeamTest
{
    interface IFileSignatureGenerator
    {
        void Generate(int blockSize, string filePath);
    }
    class FileSignatureGenerator: IFileSignatureGenerator
    {
        public void Generate(int blockSize, string filePath)
        {
            IHashGenerator generator = new HashGenerator();
            var stream = File.OpenRead(filePath);
            var len = stream.Length;
            var blocksCount = len / blockSize + 1; // + 1 block part (last block)

            Console.Write($"Processing file '{filePath}'"
                + $"({len / (1024 * 1024)}Mb)"
                + $" with {blockSize} bytes blocks."
                + $" That is {blocksCount} blocks.");

            int concurrencyLevel = 10;
            var results =  new ConcurrentDictionary<long, int>(concurrencyLevel, (int)blocksCount); //(int)blocksCount is initial, can be more than int 
            var offset = 0;
            for (int i = 0; i < blocksCount; i++)
            {
                var buffer = new byte[blockSize]; //allocate new memory
                stream.Read(buffer, offset, blockSize);
                generator.GetHashForBlock(blockNumber: i, ref results, ref buffer);
                offset += blockSize;
             }
        }
    }
}
