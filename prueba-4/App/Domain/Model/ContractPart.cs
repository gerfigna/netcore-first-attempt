using System;

namespace App.Domain.Model;


public class ContractPart
{
    private SignatureRole[] _signatures;

    private ContractPart(SignatureRole[] signatures)
    {
        _signatures = signatures;    
    }

    public static ContractPart fromString(string signaturesString)
    {
        guard(signaturesString);
        SignatureRole[] roles = signaturesString.Select(c => (SignatureRole)Enum.Parse(typeof(SignatureRole), c.ToString())).ToArray();
        return new ContractPart(roles);
    }


    private static void guard(string signatures)
    {

        foreach (char c in signatures)
        {
            if (!Enum.IsDefined(typeof(SignatureRole), c.ToString()))
            {
                throw new ArgumentException($"La cadena '{signatures}' contiene caracteres no válidos");
            }
        }
    }

    public SignatureRole[] Signatures { get => _signatures; }

    public override string ToString()
    {
        return string.Join("", Signatures.Select(s => s.ToString()));
    }
}

