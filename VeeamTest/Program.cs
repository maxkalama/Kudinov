using System.Diagnostics;
using VeeamTest;

if (args.Length != 2)
{
    Console.Write("Usage (call exe with parameters): VeeamTest [block size] [file path]. Any key to exit...");
    Console.ReadKey();
    return;
}

int blockSize = 0; //max file size is 32Gb (32 bil), assume max block size is the same, uint is only 4+ mil
string? filePath = args[1];

if (!int.TryParse(args[0], out blockSize) || blockSize <= 0)
    Console.WriteLine("Wrong block size. A positive integer is expected.");

if (!File.Exists(filePath)) //checks file path for null or zero-length
    Console.WriteLine("Wrong file path. A path to an existing read-accessable file is expected.");


try
{
    IFileSignatureGenerator generator = new FileSignatureGenerator();
    generator.Generate(blockSize, filePath);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.Write("Press Enter to close the window... ");
Console.ReadLine();