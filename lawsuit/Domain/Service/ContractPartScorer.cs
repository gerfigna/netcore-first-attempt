using Signaturit.Lawsuit.Domain.Model;

namespace Signaturit.Lawsuit.Domain.Service;

public class ContractPartScorer: IContractPartScorer
{
    public int GetScore(ContractPart part)
    {
        int score = 0;

        foreach (var signature in part.Signatures)
        {
            switch (signature)
            {
                case SignatureRole.K:
                    score += (int) SignatureRole.K;
                    break;
                case SignatureRole.N:
                    score += (int) SignatureRole.N;
                    break;
                case SignatureRole.V:
                    score += part.hasRole(SignatureRole.K) ? 0 : (int)SignatureRole.V;
                    break;
            }
        }

        return score;
    }
}

