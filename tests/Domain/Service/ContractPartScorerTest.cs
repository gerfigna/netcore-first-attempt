using System;
using Signaturit.Lawsuit.Domain.Model;
using Signaturit.Lawsuit.Domain.Service;

namespace Tests.Signaturit.Lawsuit.Domain.Service;

[TestClass]
public class ContractPartScorerTest
{

    [DataTestMethod]
    [DataRow("K", 5)]
    [DataRow("N", 2)]
    [DataRow("V", 1)]
    [DataRow("KK", 10)]
    [DataRow("KN", 7)]
    [DataRow("KV", 5)]
    [DataRow("NN", 4)]
    [DataRow("NV", 3)]
    [DataRow("VNKK", 12)]
    [DataRow("NVVVV", 6)]
    [DataRow("VVVVVVVVVNK", 7)]
    public void ScoreContractPartTest(string signatures, int score)
    {
        var scorer = new ContractPartScorer();
        var part = ContractPart.fromString(signatures);

        Assert.AreEqual(score, scorer.GetScore(part));
        
    }
}

