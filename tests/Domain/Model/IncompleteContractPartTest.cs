using System;
using lawsuit.Domain.CustomException;
using Signaturit.Lawsuit.Domain.CustomException;
using Signaturit.Lawsuit.Domain.Model;

namespace Tests.Signaturit.Lawsuit.Domain.Model;

[TestClass]
public class IncompleteContractPartTest
{
    [TestMethod]
    public void CreateIncompleteContractPartTest()
	{
        var signatures = "N#V";

        var part = IncompleteContractPart.fromString(signatures);

        var expected = new[] { SignatureRole.N, SignatureRole.V };

        CollectionAssert.AreEqual(expected, part.Signatures);
        Assert.AreEqual(signatures, part.ToString());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidCharacterException))]
    public void CreateContractPartWithExceptionTest()
    {
        var signatures = "PN#V";

        var part = IncompleteContractPart.fromString(signatures);
    }


    [TestMethod]
    [ExpectedException(typeof(InvalidIncompletePartException))]
    public void CreateContractPartWithDoubleHashExceptionTest()
    {
        var signatures = "N#V#";

        var part = IncompleteContractPart.fromString(signatures);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidIncompletePartException))]
    public void CreateContractPartWithDoubleHashAndInvalidCharacterExceptionTest()
    {
        var signatures = "NP#V#";

        var part = IncompleteContractPart.fromString(signatures);
    }
}

