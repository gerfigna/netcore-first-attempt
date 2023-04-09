using MediatR;

namespace Signaturit.Lawsuit.Application.Query.GuessSignature;

public class GuessSignatureQuery : IRequest<GuessSignatureQueryResponse>
{

    private readonly string _incomplete;
    private readonly string _opposition;

    public GuessSignatureQuery(string incompleteParty, string kownOppositionSignatures)
    {
        _incomplete = incompleteParty;
        _opposition = kownOppositionSignatures;
    }

    public virtual string IncompletePartSignatures
    {
        get { return _incomplete; }
    }

    public virtual string KonwnOppositionSignatures
    {
        get { return _opposition; }
    }
}

