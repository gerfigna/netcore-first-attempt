using MediatR;
using Signaturit.Lawsuit.Domain.Model;
using Signaturit.Lawsuit.Domain.Service;

namespace Signaturit.Lawsuit.Application.Query.TrialWinner;


public class GetTrialWinnerQueryHandler : IRequestHandler<GetTrialWinnerQuery, GetTrialWinnerQueryResponse>
{
    private ContractPartScorer _contractScorer;

    public GetTrialWinnerQueryHandler(ContractPartScorer contractScorer)
    {
        _contractScorer = contractScorer;
    }

    public Task<GetTrialWinnerQueryResponse> Handle(GetTrialWinnerQuery request, CancellationToken cancellationToken)
    {
        ContractPart plaintiff = ContractPart.fromString(request.PlaintiffSignatures);
        ContractPart defendant = ContractPart.fromString(request.DefendantSignatures);

        int firstPartyScore = _contractScorer.GetScore(plaintiff);
        int secondPartyScore = _contractScorer.GetScore(defendant);

        string message;
        int score;

        if (firstPartyScore > secondPartyScore)
        {
            message = $"Paintiff party wins with signatures '{plaintiff.ToString()}'";
            score = firstPartyScore;

        }
        else if (secondPartyScore > firstPartyScore)
        {
            message = $"Defendant party wins with signatures '{defendant.ToString()}'";
            score = secondPartyScore;
        }
        else
        {
            message = "Tie";
            score = firstPartyScore;
        }

        return Task.FromResult(new GetTrialWinnerQueryResponse(score, message));
    }
}
