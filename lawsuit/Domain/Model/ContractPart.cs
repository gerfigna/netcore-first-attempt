using Signaturit.Lawsuit.Domain.CustomException;
namespace Signaturit.Lawsuit.Domain.Model;


public class ContractPart
{
    private readonly SignatureRole[] _signatures;

    protected ContractPart(SignatureRole[] signatures)
    {
        _signatures = signatures;    
    }

    public static ContractPart fromString(string signaturesString)
    {
        Guard(signaturesString);
        SignatureRole[] roles = signaturesString.Select(c => (SignatureRole)Enum.Parse(typeof(SignatureRole), c.ToString())).ToArray();

        return new ContractPart(roles);
    }


    protected static void Guard(string signatures)
    {

        foreach (char c in signatures)
        {
            if (!Enum.IsDefined(typeof(SignatureRole), c.ToString()))
            {
                throw new InvalidCharacterException($"La cadena '{signatures}' contiene caracteres no válidos");
            }
        }
    }

    public SignatureRole[] Signatures { get => _signatures; }

    public override string ToString()
    {
        return string.Join("", Signatures.Select(s => s.ToString()));
    }

    public bool hasRole(SignatureRole r)
    {
        return Signatures.Where(s => s.Equals(r)).Count() > 0;
    }
}

