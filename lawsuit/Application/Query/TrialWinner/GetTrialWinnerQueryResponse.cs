namespace Signaturit.Lawsuit.Application.Query.TrialWinner;

public class GetTrialWinnerQueryResponse
{
    public GetTrialWinnerQueryResponse(string message)
    {
        Message = message;
    }

    public string Message { get; }
}