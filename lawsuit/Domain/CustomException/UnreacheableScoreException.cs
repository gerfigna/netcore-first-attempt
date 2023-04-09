using System;
namespace Signaturit.Lawsuit.Domain.CustomException;

public class UnreacheableScoreException : Exception
{
	public UnreacheableScoreException(string message) : base(message)
    {
	}
}

