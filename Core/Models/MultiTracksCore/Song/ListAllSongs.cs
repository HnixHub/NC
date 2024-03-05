using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.MultiTracksCore.Song
{
    public class ListAllSongsRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
    public class ListAllSongsResponse
    {
        public List<Song>? SongList { get; set; }
        public class Song
        {
            public int SongId { get; set; }
            public DateTime DateCreation { get; set; }
            public int AlbumId { get; set; }
            public int ArtistId { get; set; }
            public string? Title { get; set; }
            public Decimal Bpm { get; set; }
            public string? TimeSignature { get; set; }
            public bool MultiTracks { get; set; }
            public bool CustomMix { get; set; }
            public bool Chart { get; set; }
            public bool RehearsalMix { get; set; }
            public bool Patches { get; set; }
            public bool SongSpecificPatches { get; set; }
            public bool ProPresenter { get; set; }
        }
    }
}
