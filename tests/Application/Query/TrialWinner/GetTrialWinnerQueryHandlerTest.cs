using System;
using Moq;
using Signaturit.Lawsuit.Domain.Model;
using Signaturit.Lawsuit.Domain.Service;
using Signaturit.Lawsuit.Application.Query.TrialWinner;

namespace Tests.Signaturit.Lawsuit.Application.Query.TrialWinner;

[TestClass]
public class GetTrialWinnerQueryHandlerTest
{

    [TestMethod]
    public async Task PlaintiffWinsTest()
    {
        var scorer = new Mock<IContractPartScorer>();
        scorer.SetupSequence(m => m.GetScore(It.IsAny<ContractPart>()))
            .Returns(7)
            .Returns(5);

        var handler = new GetTrialWinnerQueryHandler(scorer.Object);

        var query = new GetTrialWinnerQuery("KNV", "NNV");

        var response = await handler.Handle(query, new CancellationToken());

        Assert.AreEqual("Plaintiff party wins with signatures 'KNV' and 7 points", response.Message);
        
    }


    [TestMethod]
    public async Task DefendantWinsTest()
    {
        var scorer = new Mock<IContractPartScorer>();
        scorer.SetupSequence(m => m.GetScore(It.IsAny<ContractPart>()))
            .Returns(5)
            .Returns(8);

        var handler = new GetTrialWinnerQueryHandler(scorer.Object);

        var query = new GetTrialWinnerQuery("K", "NNNN");

        var response = await handler.Handle(query, new CancellationToken());

        Assert.AreEqual("Defendant party wins with signatures 'NNNN' and 8 points", response.Message);

    }

    [TestMethod]
    public async Task TieTest()
    {
        var scorer = new Mock<IContractPartScorer>();
        scorer.SetupSequence(m => m.GetScore(It.IsAny<ContractPart>()))
            .Returns(10)
            .Returns(10);

        var handler = new GetTrialWinnerQueryHandler(scorer.Object);

        var query = new GetTrialWinnerQuery("KK", "NNNNN");

        var response = await handler.Handle(query, new CancellationToken());

        Assert.AreEqual("Tie at 10 points", response.Message);
    }
}

