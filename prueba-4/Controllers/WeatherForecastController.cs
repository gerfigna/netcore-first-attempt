using MediatR;
using Microsoft.AspNetCore.Mvc;
using Signaturit.App.Application.Query;

namespace prueba_4.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator, ILogger<WeatherForecastController> logger)
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

