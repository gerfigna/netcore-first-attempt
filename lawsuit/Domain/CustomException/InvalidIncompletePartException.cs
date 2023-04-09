using System;
namespace lawsuit.Domain.CustomException
{
	public class InvalidIncompletePartException: Exception
	{
		public InvalidIncompletePartException(string message): base(message)
		{
		}
	}
}

