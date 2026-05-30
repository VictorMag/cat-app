using CatApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatApi.Controllers;

[ApiController]
[Route("api/cats")]
public class CatsController(TheCatApiClient catApiClient) : ControllerBase
{
    [HttpGet("breeds")]
    public async Task<IActionResult> GetBreeds(CancellationToken cancellationToken)
    {
        try
        {
            var breeds = await catApiClient.GetBreedsAsync(cancellationToken);
            return Ok(breeds ?? []);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status499ClientClosedRequest);
        }
        catch (HttpRequestException ex)
        {
            return Problem(ex.Message, statusCode: StatusCodes.Status502BadGateway);
        }
    }

    [HttpGet("images")]
    public async Task<IActionResult> GetImages([FromQuery] string breedId, [FromQuery] int limit = 10, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(breedId))
            return BadRequest("El breedId es requerido");

        try
        {
            var images = await catApiClient.GetImagesByBreedAsync(breedId, limit, cancellationToken);
            return Ok(images ?? []);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status499ClientClosedRequest);
        }
        catch (HttpRequestException ex)
        {
            return Problem(ex.Message, statusCode: StatusCodes.Status502BadGateway);
        }
    }
}
