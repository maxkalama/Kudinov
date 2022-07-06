if (args.Length != 2)
{
    Console.Write("Usage (call exe with parameters): VeeamTest [block size] [file path]. Any key to exit...");
    Console.ReadKey();
    return;
}

long blockSize = 0; //max file size is 32Gb (32 bil), assume max block size is the same, uint is only 4+ mil
string? filePath = args[1];

if (!long.TryParse(args[0], out blockSize) || blockSize <= 0)
    Console.WriteLine("Wrong block size. A positive integer is expected.");

if (!File.Exists(filePath)) //checks file path for null or zero-length
    Console.WriteLine("Wrong file path. A path to an existing read-accessable file is expected.");

try
{
    VeeamTest.FileSignatureGenerator.Generate(blockSize, filePath);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}