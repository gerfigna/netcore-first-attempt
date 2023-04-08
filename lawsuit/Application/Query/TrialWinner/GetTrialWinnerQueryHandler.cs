using MediatR;
using Signaturit.Lawsuit.Domain.Model;
using Signaturit.Lawsuit.Domain.Service;

namespace Signaturit.Lawsuit.Application.Query.TrialWinner;


public class GetTrialWinnerQueryHandler : IRequestHandler<GetTrialWinnerQuery, GetTrialWinnerQueryResponse>
{
    private IContractPartScorer _contractScorer;

    public GetTrialWinnerQueryHandler(IContractPartScorer contractScorer)
    {
        _contractScorer = contractScorer;
    }

    public async Task<GetTrialWinnerQueryResponse> Handle(GetTrialWinnerQuery request, CancellationToken cancellationToken)
    {
        ContractPart plaintiff = ContractPart.fromString(request.PlaintiffSignatures);
        ContractPart defendant = ContractPart.fromString(request.DefendantSignatures);

        int firstPartyScore = _contractScorer.GetScore(plaintiff);
        int secondPartyScore = _contractScorer.GetScore(defendant);

        string message;

        if (firstPartyScore > secondPartyScore)
        {
            message = $"Plaintiff party wins with signatures '{plaintiff.ToString()}' and {firstPartyScore} points";

        }
        else if (secondPartyScore > firstPartyScore)
        {
            message = $"Defendant party wins with signatures '{defendant.ToString()}' and {secondPartyScore} points";
        }
        else
        {
            message = $"Tie at {firstPartyScore} points";
        }

        return await Task.FromResult(new GetTrialWinnerQueryResponse(message));
    }
}
