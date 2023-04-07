namespace Signaturit.Lawsuit.Domain.CustomException;

public class InvalidCharacterException: ArgumentException
{
	public InvalidCharacterException(string message): base(message)
	{
	}
}

