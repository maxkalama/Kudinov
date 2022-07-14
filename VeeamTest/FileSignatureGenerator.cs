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

            int taskCount = 0;
            Task? task = null;
            ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);
            Console.WriteLine($"Min workerThreads {workerThreads}, completionPortThreads {completionPortThreads}");
            ThreadPool.SetMinThreads(100, 100); //task scheduler to allow threads number more than actual CPUs number
            for (long i = 0; i < blocksCount; i++)
            {
                var buffer = new byte[blockSize]; //allocate new memory
                stream.Read(buffer, 0, blockSize);

                //store parameters for correct thread call
                long blockNumber = i;
                Task? waitTask = task;
                task = Task.Run(() => generator.GetHashForBlock(blockNumber, waitTask, ref buffer, ref taskCount));
            }

            task?.Wait();

            sw.Stop();
            Console.WriteLine($"Generation has taken { sw.ElapsedMilliseconds / (decimal)1000} seconds.");
        }
    }
}
