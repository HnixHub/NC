using Microsoft.AspNetCore.Mvc;
using Services.MultiTracksCore.Artist;
using Models.MultiTracksCore.Artist;
using api.multitracks.com.Controllers;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : BaseController
    {
        private ArtistService _artistService;

        public ArtistController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchArtistRequest rq)
        {
            return ApiResult(await _artistService.SearchArtist(rq));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromQuery] CreateArtistRequest rq)
        {
            return ApiResult(await _artistService.CreateArtist(rq));
        }
    }
}
