using System.Diagnostics;

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
            Stopwatch sw = new Stopwatch();
            sw.Start();

            IHashGenerator generator = new HashGenerator();
            using var stream = File.OpenRead(filePath);
            var len = stream.Length;

            if (len == 0)
                throw new ApplicationException("File size is zero. Please try another one.");

            var blocksCount = len / blockSize;
            
            if (len % blockSize != 0)
                blocksCount++; // + 1 block part (last block)

            Console.WriteLine($"Processing file '{filePath}'"
                + $"({len / (1024 * 1024)}Mb)"
                + $" with {blockSize} bytes blocks."
                + $" That is {blocksCount} blocks.");

            int threadCount = 0;
            Thread? previousThread = null;
            for (long i = 0; i < blocksCount; i++)
            {
                var buffer = new byte[blockSize]; //allocate new memory
                stream.Read(buffer, 0, blockSize);

                //store parameters for correct thread call
                long blockNumber = i;
                Thread? waitForThread = previousThread;

                var thread = new Thread(() => generator.GetHashForBlock(blockNumber, waitForThread, ref buffer, ref threadCount));
                thread.Name = $"Block {blockNumber}";
                thread.Start();
                previousThread = thread;
            }

            previousThread?.Join();

            sw.Stop();
            Console.WriteLine($"Generation has taken { sw.ElapsedMilliseconds / (decimal)1000} seconds.");
        }
    }
}
