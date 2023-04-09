using System;
using Moq;
using Signaturit.Lawsuit.Domain.Model;
using Signaturit.Lawsuit.Domain.Service;
using Signaturit.Lawsuit.Application.Query.GuessSignature;
using Signaturit.Lawsuit.Domain.CustomException;

namespace Tests.Signaturit.Lawsuit.Application.Query.GuessSignature;

[TestClass]
public class GuessSignatureQueryHandlerTest
{

    [TestMethod]
    public async Task NeedsANotarySignature()
    {
        var scorer = new Mock<IContractPartScorer>();
        scorer.SetupSequence(m => m.GetScore(It.IsAny<ContractPart>()))
            .Returns(3)
            .Returns(4);

        var handler = new GuessSignatureQueryHandler(scorer.Object);

        var query = new GuessSignatureQuery("N#V", "NVV");

        var response = await handler.Handle(query, new CancellationToken());

        Assert.AreEqual("'N#V' needs a N to win", response.Message);
        
    }


    [TestMethod]
    [ExpectedException(typeof(UnreacheableScoreException))]
    public async Task NoSignatureWinsTest()
    {
        var scorer = new Mock<IContractPartScorer>();
        scorer.SetupSequence(m => m.GetScore(It.IsAny<ContractPart>()))
            .Returns(2)
            .Returns(8);

        var handler = new GuessSignatureQueryHandler(scorer.Object);

        var query = new GuessSignatureQuery("N#", "NNNN");

        var response = await handler.Handle(query, new CancellationToken());
    }

    [DataTestMethod]
    [DataRow(3, 3, "V")]
    [DataRow(3, 4, "N")]
    [DataRow(3, 7, "K")]
    public async Task GuessTest(int incomplete, int oppostion, string need)
    {
        var scorer = new Mock<IContractPartScorer>();
        scorer.SetupSequence(m => m.GetScore(It.IsAny<ContractPart>()))
            .Returns(incomplete)
            .Returns(oppostion);

        var handler = new GuessSignatureQueryHandler(scorer.Object);

        var query = new Mock<GuessSignatureQuery>("N#V", "NVV");
        query.SetupGet(q => q.IncompletePartSignatures).Returns("N#V");
        query.SetupGet(q => q.KonwnOppositionSignatures).Returns("NVV");

        var response = await handler.Handle(query.Object, new CancellationToken());

        Assert.AreEqual($"'N#V' needs a {need} to win", response.Message);
    }
}
