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
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Data Missing", "Enter the playlist name.");
            }
            else
            {
                string username = "HansenB";  // username will come from security once implemented

                //MessageUserControl will be used to handle the code behind user friendly error handling
                //you will NOT be using try/catch
                //your try/catch is embedded within the MessageUserControl class
                MessageUserControl.TryRun(() =>
                {
                    //coding block
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                },"PlayList","Manage your playlist"); //strings after coding block is for success
            }
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
            if(string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name");
            }
            else
            {
                string username = "HansenB";
                MessageUserControl.TryRun(() =>
                {
                    int trackid = int.Parse(e.CommandArgument.ToString());
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, trackid);
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    PlayList.DataSource = info;
                    PlayList.DataBind();
                },"Add Track","Track has been added to your playlist");
            }
            
            
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