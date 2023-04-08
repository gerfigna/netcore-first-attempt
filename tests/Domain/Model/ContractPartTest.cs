namespace Tests.Signaturit.Lawsuit.Domain.Model;

using global::Signaturit.Lawsuit.Domain.CustomException;
using global::Signaturit.Lawsuit.Domain.Model;

[TestClass]
public class ContractPartTest
{
    [TestMethod]
    public void CreateContractPartTest()
    {
        var signatures = "KNV";

        var part = ContractPart.fromString(signatures);
        var expected = new[] { SignatureRole.K, SignatureRole.N, SignatureRole.V };

        CollectionAssert.AreEqual(expected, part.Signatures);
        Assert.AreEqual(signatures, part.ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidCharacterException))]
    public void CreateContractPartWithExceptionTest()
    {
        var signatures = "PNV";

        var part = ContractPart.fromString(signatures);       
    }

    [DataTestMethod]
    [DataRow("K", "K", true)]
    [DataRow("N", "K", false)]
    [DataRow("V", "K", false)]
    [DataRow("KK", "K", true)]
    [DataRow("KN", "K", true)]
    [DataRow("KV", "K", true)]
    [DataRow("NN", "K", false)]
    [DataRow("NV", "K", false)]
    [DataRow("VNKK", "K", true)]
    [DataRow("NVVVV", "K", false)]
    [DataRow("VVVVVVVVVNK", "K", true)]
    public void CheckHasRoleTest(string signatures, string roleToAsk, bool expected)
    {

        var part = ContractPart.fromString(signatures);

        Assert.AreEqual(expected, part.hasRole((SignatureRole)Enum.Parse(typeof(SignatureRole), roleToAsk)));
    }
}
