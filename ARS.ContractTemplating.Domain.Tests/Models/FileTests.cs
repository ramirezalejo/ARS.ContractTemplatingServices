using NUnit.Framework;

namespace ARS.ContractTemplating.Domain.Tests.Models;
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
            ContentType = contenType
        };
        Assert.AreEqual(isOk, file.IsOkToCompress());
        
    }
}