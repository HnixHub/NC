using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.MultiTracksCore.Artist
{
    public class CreateArtistRequest
    {
        public string? Title { get; set; }
        public string? Biography { get; set; }
        public string? ImageURL { get; set; }
        public string? HeroURL { get; set; }
    }
    public class CreateArtistResponse
    {
        public string? Title { get; set; }
    }
}
