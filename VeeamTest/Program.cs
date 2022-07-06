int blockSize = 0;
string? filePath = null;

if (args.Length != 2)
    Console.WriteLine("Usage: VeeamTest [block size] [file path]");

if (!int.TryParse(args[0], out blockSize) || blockSize <= 0)
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