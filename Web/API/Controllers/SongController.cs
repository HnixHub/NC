using Microsoft.AspNetCore.Mvc;
using Models.MultiTracksCore.Artist;
using Models.MultiTracksCore.Song;
using Services.MultiTracksCore.Artist;
using Services.MultiTracksCore.Song;

namespace api.multitracks.com.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongController : BaseController
    {
        private SongService _songService;

        public SongController(SongService artistService)
        {
            _songService = artistService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List([FromQuery] ListAllSongsRequest rq)
        {
            return ApiResult(await _songService.ListAllSongs(rq));
        }
    }
}
