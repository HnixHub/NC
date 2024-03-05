using api.multitracks.com.Models;
using DataAccess;
using Models.MultiTracksCore.Artist;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using api.multitracks.com.MappingExtensions;
using System.Net.Sockets;

namespace Services.MultiTracksCore.Artist
{
    public class ArtistService
    {
        #region Constructor
        private readonly SQL _sql;

        public ArtistService()
        {
            _sql = new SQL();
        }
        #endregion

        #region Methods
        public async Task<ServiceResponse<SearchArtistResponse>> SearchArtist(SearchArtistRequest rq)
        {
            var sr = new ServiceResponse<SearchArtistResponse>();

            try
            {
                // Validate request
                sr.Attach(ValidateSearch(rq));

                if (!sr.Status)
                    return sr;

                var artistList = GetArtistByName(rq.ArtistName);

                var query = EFExtensions.ToListAsyncSafe<SearchArtistResponse.Artist>(artistList.AsQueryable());

                sr.Data = new SearchArtistResponse
                {
                    ArtistList = await query
                };
            }
            catch (Exception ex)
            {
                sr.AddError($"An error ocurred while searching an Artist, Exception:{ex}");
            }

            return sr;
        }

        public Task<ServiceResponse<CreateArtistResponse>> CreateArtist(CreateArtistRequest rq)
        {
            var sr = new ServiceResponse<CreateArtistResponse>();

            // Validate request
            sr.Attach(ValidateCreate(rq));

            if (!sr.Status)
                return Task.FromResult(sr);

            _sql.OpenConnection();
            _sql.BeginTransaction();

            _sql.Parameters.Clear();

            _sql.Parameters.Add("@Title", rq.Title);
            _sql.Parameters.Add("@DateCreation", DateTime.Now);
            _sql.Parameters.Add("@Biography", rq.Biography);
            _sql.Parameters.Add("@ImageURL", rq.ImageURL);
            _sql.Parameters.Add("@HeroURL", rq.HeroURL);

            try
            {
                _sql.Execute("INSERT INTO [dbo].[Artist] ([dateCreation], [title], [biography], [imageURL], [heroURL]) VALUES(@DateCreation, @Title, @Biography, @ImageURL, @HeroURL)");

                _sql.Commit();
                _sql.CloseConnection();
            }
            catch (Exception)
            {
                _sql.Rollback();
                return Task.FromResult(sr.AddError("Error adding a new artist"));
            }
            
            sr.Data = new CreateArtistResponse
            {
                Title = rq.Title
            };

            sr.Message = "Artist succesfully added";

            return Task.FromResult(sr);
        }
        #endregion

        #region Helpers
        private List<SearchArtistResponse.Artist> GetArtistByName(string? artistName)
        {
            _sql.Parameters.Clear();

            _sql.Parameters.Add("@ArtistName", artistName);

            DataTable dt = _sql.ExecuteStoredProcedureDT("GetArtistByName", false);

            var artistList = ((from row in dt.AsEnumerable()
                               select new SearchArtistResponse.Artist
                               {
                                   ArtistId = row.Field<int>("artistID"),
                                   DateCreation = row.Field<DateTime>("dateCreation"),
                                   Title = row.Field<string>("title"),
                                   Biography = row.Field<string>("biography"),
                                   ImageURL = row.Field<string>("imageURL"),
                                   HeroURL = row.Field<string>("heroURL")
                               })).ToList();

            return artistList;
        }
        #endregion

        #region Validations
        private ServiceResponse ValidateSearch(SearchArtistRequest rq)
        {
            var sr = new ServiceResponse();

            // ArtistName
            if (string.IsNullOrEmpty(rq.ArtistName))
                return sr.AddError("Input search is empty");

            return sr;
        }

        private ServiceResponse ValidateCreate(CreateArtistRequest rq)
        {
            var sr = new ServiceResponse();

            // ArtistName
            if (string.IsNullOrEmpty(rq.Title))
                return sr.AddError("Artist name field is empty");

            if (string.IsNullOrEmpty(rq.Biography))
                return sr.AddError("Biography field is empty");

            if (string.IsNullOrEmpty(rq.ImageURL))
                return sr.AddError("ImageURL field is empty");

            if (string.IsNullOrEmpty(rq.HeroURL))
                return sr.AddError("HeroURL field is empty");

            if (GetArtistByName(rq.Title).Count > 0)
                return sr.AddError($"{rq.Title} already exists");

            return sr;
        }
        #endregion
    }
}