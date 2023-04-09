using MediatR;
using Microsoft.AspNetCore.Mvc;
using Signaturit.Lawsuit.Application.Query.TrialWinner;
using Signaturit.Lawsuit.Domain.CustomException;

namespace Signaturit.Lawsuit.UI;

[ApiController]
[Route("api")]
public class TrialController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrialController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("winner")]
    public async Task<ActionResult<GetTrialWinnerQueryResponse>> Get([FromQuery] string plaintiff, [FromQuery] string defendant)
    {
        try
        {
            return await _mediator.Send(new GetTrialWinnerQuery(plaintiff, defendant));
        }
        catch(InvalidCharacterException _e)
        {
            return new BadRequestObjectResult(_e.Message);
        }

    }
}
