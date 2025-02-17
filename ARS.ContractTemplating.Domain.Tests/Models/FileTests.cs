using NUnit.Framework;

namespace ARS.ContractTemplating.Domain.Tests.Models;
[TestFixture]
public class FileTests
{
    [Test]
    [TestCase("application/x-abiword", true)]
    [TestCase("octet-stream", false)]
    [TestCase("application/octet-stream", true)]
    [TestCase("application/x-abiword", true)]
    [TestCase("application/x-abiword-compressed", false)]
    [TestCase("application/x-abiword-compressed-tar", false)]
    [TestCase("application/vnd.ms-excel", true)]
    [TestCase("application/Test", false)]
    [TestCase("image/svg+xml", true)]
    public void File_IsOkToCompress(string contenType, bool isOk)
    {
        var file = new ARS.ContractTemplating.Domain.Models.File()
        {
            ContentType = contenType,
            FileName = "Test1",
            Owner = "Me",
            State = 1,
            OriginalSize = 1,
            CompressedSize = 1
        };
        Assert.AreEqual(isOk, file.IsOkToCompress());
        
    }
}