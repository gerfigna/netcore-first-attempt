using MediatR;
using Microsoft.AspNetCore.Mvc;
using Signaturit.Lawsuit.Application.Query;

namespace Signaturit.Lawsuit.UI;

[ApiController]
[Route("[controller]")]
public class TrialController : ControllerBase
{
    private readonly ILogger<TrialController> _logger;
    private readonly IMediator _mediator;

    public TrialController(IMediator mediator, ILogger<TrialController> logger)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public Task<GetTrialWinnerQueryResponse> Get([FromQuery] string plaintiff, [FromQuery] string defendant)
    {
        return _mediator.Send(new GetTrialWinnerQuery(plaintiff, defendant));
      
    }
}

