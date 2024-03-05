using api.multitracks.com.MappingExtensions;
using api.multitracks.com.Models;
using DataAccess;
using Models.MultiTracksCore.Artist;
using Models.MultiTracksCore.Song;
using System.Data;

namespace Services.MultiTracksCore.Song
{
    public class SongService
    {
        #region Constructor
        private readonly SQL _sql;

        public SongService()
        {
            _sql = new SQL();
        }
        #endregion

        #region Methods
        public async Task<ServiceResponse<ListAllSongsResponse>> ListAllSongs(ListAllSongsRequest rq)
        {
            var sr = new ServiceResponse<ListAllSongsResponse>();

            sr.Attach(ValidateSongsRequest(rq));

            if (!sr.Status)
                return sr;

            var pagedSongList = GetPagedSongs(GetAllSongs(), rq.PageSize, rq.PageIndex);

            var query = EFExtensions.ToListAsyncSafe<ListAllSongsResponse.Song>(pagedSongList.AsQueryable());

            sr.Data = new ListAllSongsResponse
            {
                SongList = await query
            };

            return sr;
        }
        #endregion

        #region Helpers
        private List<ListAllSongsResponse.Song> GetAllSongs()
        {
            var dt = _sql.ExecuteDT("SELECT * FROM Song ORDER BY title");

            var songList = ((from row in dt.AsEnumerable()
                               select new ListAllSongsResponse.Song
                               {
                                   SongId = row.Field<int>("songId"),
                                   DateCreation = row.Field<DateTime>("dateCreation"),
                                   AlbumId = row.Field<int>("albumId"),
                                   ArtistId = row.Field<int>("artistID"),
                                   Title = row.Field<string>("title"),
                                   Bpm = row.Field<Decimal>("bpm"),
                                   TimeSignature = row.Field<string>("timeSignature"),
                                   MultiTracks = row.Field<bool>("multitracks"),
                                   CustomMix = row.Field<bool>("customMix"),
                                   Chart = row.Field<bool>("chart"),
                                   RehearsalMix = row.Field<bool>("rehearsalMix"),
                                   Patches = row.Field<bool>("patches"),
                                   SongSpecificPatches = row.Field<bool>("songSpecificPatches"),
                                   ProPresenter = row.Field<bool>("proPresenter"),
                               })).ToList();

            return songList;
        }

        //This method should be created on a Helper with dynamic data
        private IList<ListAllSongsResponse.Song> GetPagedSongs(IList<ListAllSongsResponse.Song> songList, int pageSize, int pageNumber)
        {
            return songList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }


        #endregion

        #region Validations
        private ServiceResponse ValidateSongsRequest(ListAllSongsRequest rq)
        {
            var sr = new ServiceResponse();

            // Page Index
            if (rq.PageIndex < 0)
                return sr.AddError("Page index must be a positive number");

            // Page Size
            if (rq.PageSize < 0)
                return sr.AddError("Page size must be a positive number");

            return sr;
        }
        #endregion
    }
}
