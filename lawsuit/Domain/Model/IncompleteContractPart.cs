using System;
using lawsuit.Domain.CustomException;
using Signaturit.Lawsuit.Domain.CustomException;

namespace Signaturit.Lawsuit.Domain.Model;

public class IncompleteContractPart: ContractPart
{
    private readonly string original;

    protected IncompleteContractPart(SignatureRole[] signatures, string _original) : base(signatures)
    {
        original = _original;
    }

    public static new IncompleteContractPart fromString(string signaturesString)
    {
        Guard(signaturesString);

        SignatureRole[] roles = signaturesString
            .Where(c => "#" != c.ToString())
            .Select(c => (SignatureRole)Enum.Parse(typeof(SignatureRole), c.ToString())).ToArray()
        ;

        return new IncompleteContractPart(roles, signaturesString);
    }


    protected static new void Guard(string signatures)
    {
        if(1 != signatures.Where(s => "#" == s.ToString()).Count())
        {
            throw new InvalidIncompletePartException($"La cadena '{signatures}' debe tener una ocurrencia del caracter #");
        }

        ContractPart.Guard(signatures.Replace("#", ""));
    }

    public override string ToString()
    {
        return original;
    }
}

