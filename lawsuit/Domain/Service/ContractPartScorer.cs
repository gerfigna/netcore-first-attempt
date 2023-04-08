using Signaturit.Lawsuit.Domain.Model;

namespace Signaturit.Lawsuit.Domain.Service;

public class ContractPartScorer: IContractPartScorer
{
    private const int KingPoints = 5;
    private const int NotaryPoints = 2;
    private const int ValidatorPoints = 1;

    public int GetScore(ContractPart part)
    {
        int score = 0;

        foreach (var signature in part.Signatures)
        {
            switch (signature)
            {
                case SignatureRole.K:
                    score += KingPoints;
                    break;
                case SignatureRole.N:
                    score += NotaryPoints;
                    break;
                case SignatureRole.V:
                    score += part.hasRole(SignatureRole.K) ? 0 : ValidatorPoints;
                    break;
            }
        }

        return score;
    }
}

