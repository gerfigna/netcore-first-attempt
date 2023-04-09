using MediatR;
using Signaturit.Lawsuit.Domain.CustomException;
using Signaturit.Lawsuit.Domain.Model;
using Signaturit.Lawsuit.Domain.Service;

namespace Signaturit.Lawsuit.Application.Query.GuessSignature;


public class GuessSignatureQueryHandler : IRequestHandler<GuessSignatureQuery, GuessSignatureQueryResponse>
{
    private IContractPartScorer _contractScorer;

    public GuessSignatureQueryHandler(IContractPartScorer contractScorer)
    {
        _contractScorer = contractScorer;
    }

    public Task<GuessSignatureQueryResponse> Handle(GuessSignatureQuery request, CancellationToken cancellationToken)
    {
        ContractPart incomplete = IncompleteContractPart.fromString(request.IncompletePartSignatures);
        ContractPart opposition = ContractPart.fromString(request.KonwnOppositionSignatures);

        int incompleteScore = _contractScorer.GetScore(incomplete);
        int oppositionScore = _contractScorer.GetScore(opposition);

        var roles =  Enum.GetValues(typeof(SignatureRole)).Cast<SignatureRole>();
        var sortedRoles = roles.OrderBy(v => v);

        foreach (SignatureRole role in sortedRoles)
        {
            if(incompleteScore + (int) role > oppositionScore)
            {
                return Task.FromResult(new GuessSignatureQueryResponse($"'{request.IncompletePartSignatures}' needs a {role} to win")); 
            }
        }

        throw new UnreacheableScoreException($"No signature found for '{request.IncompletePartSignatures}' wins to '{request.KonwnOppositionSignatures}'");           
    }
}
