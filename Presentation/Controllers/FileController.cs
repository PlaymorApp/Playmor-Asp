using Microsoft.AspNetCore.Mvc;

namespace Playmor_Asp.Presentation.Controllers;
[Route("api")]
[ApiController]
public class FileController : Controller
{
    [HttpGet("proxy-image")]
    public async Task<IActionResult> ProxyImage(string imageUrl)
    {
        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(imageUrl);
            var content = await response.Content.ReadAsByteArrayAsync();

            return File(content, "image/jpeg");
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
