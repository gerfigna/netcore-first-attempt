using System;

namespace Signaturit.Lawsuit.Domain.CustomException
{
	public class InvalidCharacterException: Exception
	{
		public InvalidCharacterException(string message): base(message)
		{
		}
	}
}

