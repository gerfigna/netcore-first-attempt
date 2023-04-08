using System;
using Signaturit.Lawsuit.Domain.Model;

namespace Signaturit.Lawsuit.Domain.Service;

public interface IContractPartScorer
{
    public int GetScore(ContractPart part);
}

