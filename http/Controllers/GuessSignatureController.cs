using MediatR;
using Microsoft.AspNetCore.Mvc;
using Signaturit.Lawsuit.Application.Query.GuessSignature;
using Signaturit.Lawsuit.Domain.CustomException;

namespace Signaturit.Lawsuit.UI;

[ApiController]
[Route("api")]
public class GuessSignatureController : ControllerBase
{
    private readonly IMediator _mediator;

    public GuessSignatureController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("guess_sign")]
    public async Task<ActionResult<GuessSignatureQueryResponse>> GuessSignature([FromQuery] string incomplete, [FromQuery] string opposition)
    {
        try
        {
            return await _mediator.Send(new GuessSignatureQuery(incomplete, opposition));
        }
        catch(InvalidCharacterException _e)
        {
            return new BadRequestObjectResult(_e.Message);
        }

    }
}
