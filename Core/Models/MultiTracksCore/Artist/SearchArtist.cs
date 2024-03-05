namespace Models.MultiTracksCore.Artist
{
    public class SearchArtistRequest
    {
        public string? ArtistName { get; set; }
    }
    public class SearchArtistResponse
    {
        public List<Artist>? ArtistList { get; set; }
        public class Artist
        {
            public int ArtistId { get; set; }
            public DateTime DateCreation { get; set; }
            public string? Title { get; set; }
            public string? Biography { get; set; }
            public string? ImageURL { get; set; }
            public string? HeroURL { get; set; }
        }
    }
}
