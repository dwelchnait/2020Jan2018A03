using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.POCOs;

#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("Select Error", "You must enter an artist name or part of");
                TracksBy.Text = "";
                SearchArg.Text = "";
            }
            else
            {
                TracksBy.Text = "Artist";
                SearchArg.Text = ArtistName.Text;
                TracksSelectionList.DataBind();  //force an ODS to re-execute
            }
        }


        protected void GenreFetch_Click(object sender, EventArgs e)
        {

                TracksBy.Text = "Genre";
                SearchArg.Text = GenreDDL.SelectedValue;
                TracksSelectionList.DataBind();  //force an ODS to re-execute
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("Select Error", "You must enter an album title or part of");
                TracksBy.Text = "";
                SearchArg.Text = "";
            }
            else
            {
                TracksBy.Text = "Album";
                SearchArg.Text = AlbumTitle.Text;
                TracksSelectionList.DataBind();  //force an ODS to re-execute
            }
        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //code to go here
            
        }

        protected void MediaTypeDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MediaTypeDDL.SelectedIndex == 0)
            {
                MessageUserControl.ShowInfo("Select Error", "You must select a media type");
                TracksBy.Text = "";
                SearchArg.Text = "";
            }
            else
            {
                TracksBy.Text = "MediaType";
                SearchArg.Text = MediaTypeDDL.SelectedValue;
                TracksSelectionList.DataBind();  //force an ODS to re-execute
            }
        }
    }
}