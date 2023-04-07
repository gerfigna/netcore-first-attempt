namespace Signaturit.Lawsuit.Application.Query.TrialWinner;

public class GetTrialWinnerQueryResponse
{
    public GetTrialWinnerQueryResponse(int points, string signatures)
    {
        Points = points;
        Signatures = signatures;
    }

    public int Points { get; set; }
    public string Signatures { get; }
}