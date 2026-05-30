using CatApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatApi.Controllers;

[ApiController]
[Route("api/cats")]
public class CatsController(TheCatApiClient catApiClient) : ControllerBase
{
    [HttpGet("breeds")]
    public async Task<IActionResult> GetBreeds()
    {
        try
        {
            var breeds = await catApiClient.GetBreedsAsync();
            return Ok(breeds ?? []);
        }
        catch (HttpRequestException ex)
        {
            return Problem(ex.Message, statusCode: StatusCodes.Status502BadGateway);
        }
    }

    [HttpGet("images")]
    public async Task<IActionResult> GetImages([FromQuery] string breedId, [FromQuery] int limit = 10)
    {
        if (string.IsNullOrWhiteSpace(breedId))
            return BadRequest("El breedId es requerido");

        try
        {
            var images = await catApiClient.GetImagesByBreedAsync(breedId, limit);
            return Ok(images ?? []);
        }
        catch (HttpRequestException ex)
        {
            return Problem(ex.Message, statusCode: StatusCodes.Status502BadGateway);
        }
    }
}
