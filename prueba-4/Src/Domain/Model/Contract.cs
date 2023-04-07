using System;

namespace Signaturit.Lawsuit.Domain.Model;

public class Contract
{
    private ContractPart _plaintiff;

    private ContractPart _defendant;

    public Contract(ContractPart plaintiff, ContractPart defendant)
    {
        _plaintiff = plaintiff;
        _defendant = defendant;
    }
}