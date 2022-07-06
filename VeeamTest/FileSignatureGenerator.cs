using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeeamTest
{

    static class FileSignatureGenerator
    {
        public static void Generate(int blockSize, string filePath)
        {
            var stream = File.OpenRead(filePath);            
        }
    }
}
