namespace Signaturit.Lawsuit.Application.Query.GuessSignature;

public class GuessSignatureQueryResponse
{
    public GuessSignatureQueryResponse(string message)
    {
        Message = message;
    }

    public string Message { get; }
}