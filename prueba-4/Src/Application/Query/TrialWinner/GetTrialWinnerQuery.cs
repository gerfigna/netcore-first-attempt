using MediatR;

namespace Signaturit.Lawsuit.Application.Query.TrialWinner;

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

