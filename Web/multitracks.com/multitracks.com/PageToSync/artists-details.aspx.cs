using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.DynamicData;
using System.Web.UI.WebControls;

public partial class Default : MultitracksPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropdown();
        }
    }

    private void LoadArtist(int id)
    {
        var tables = new string[] { "details", "topSongs", "albums" };

        var sql = new SQL();

        sql.Parameters.Add("@artistID", id);
        var data = sql.ExecuteStoredProcedureDS("GetArtistDetails").SetTableNames(tables);



        details.DataSource = data.Tables["details"];
        details.DataBind();

        topSongs.DataSource = data.Tables["topSongs"];
        topSongs.DataBind();

        albums.DataSource = data.Tables["albums"];
        albums.DataBind();


        var biography = data.Tables["details"].Rows[0].Field<string>("Biography");

        artistBiography.Text = biography;

        artistPanel.Visible = true;
    }

    private void LoadDropdown()
    {
        var sql = new SQL();

        var artistData = sql.ExecuteDS("Select artistID, title from Artist");

        DataView dv = new DataView(artistData.Tables[0]);

        artistList.DataSource = dv;

        artistList.DataValueField = "artistID";
        artistList.DataTextField = "title";


        artistList.DataBind();
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {

        LoadArtist(int.Parse(this.artistList.SelectedValue));

    }
}
