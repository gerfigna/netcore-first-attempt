using MediatR;
using App.Domain.Model;
using Signaturit.App.Domain.Service;

namespace Signaturit.App.Application.Query;


public class GetTrialWinnerQuery : IRequest<GetTrialWinnerQueryResponse>
{
    public GetTrialWinnerQuery(string plaintiffSignatures, string defendantSignatures)
    {
        PlaintiffSignatures = plaintiffSignatures;
        DefendantSignatures = defendantSignatures;
    }

    public string PlaintiffSignatures { get; }
    public string DefendantSignatures { get; }
}

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

public class GetTrialWinnerQueryResponse
{
    public GetTrialWinnerQueryResponse(int points, string signatures)
    {
        Points = points;
        Signatures = signatures;
    }

    public int Points { get; set; }
    public string Signatures { get; }
}