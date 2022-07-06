using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeeamTest
{

    static class FileSignatureGenerator
    {
        public static void Generate(long blockSize, string filePath)
        {            
            var stream = File.OpenRead(filePath);
            var length = stream.Length;
            var blocksCount = length / blockSize;
            Console.Write($"Processing file '{filePath}'"
                + $"({length / (1024 * 1024)}Mb)"
                + $" with {blockSize} bytes blocks."
                + $" That is {blocksCount} blocks.");
        }
    }
}
