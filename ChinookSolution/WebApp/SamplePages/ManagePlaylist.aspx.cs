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
            if (PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Moving Tracks", 
                    "You must have a playlist showing. List either empty or you need to fetch your playlist.");
            }
            else
            {
                if(string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Missing Data",
                        "You need a playlist name.");
                }
                else
                {
                    //validation about the move
                    //only a single song selected
                    //not the last item on the list
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for(int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        if (playlistselection.Checked)
                        {
                            rowselected++;
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                        }
                    }

                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Warning", "Select one track to be moved");
                    }
                    else
                    {
                        if (tracknumber == PlayList.Rows.Count)
                        {
                            MessageUserControl.ShowInfo("Warning", "Track cannot be moved down");
                        }
                        else
                        {
                            MoveTrack(trackid, tracknumber, "down");
                        }
                    }
                }
            }
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            if (PlayList.Rows.Count == 0)
            {
                MessageUserControl.ShowInfo("Moving Tracks",
                    "You must have a playlist showing. List either empty or you need to fetch your playlist.");
            }
            else
            {
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    MessageUserControl.ShowInfo("Missing Data",
                        "You need a playlist name.");
                }
                else
                {
                    //validation about the move
                    //only a single song selected
                    //not the last item on the list
                    int trackid = 0;
                    int tracknumber = 0;
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        if (playlistselection.Checked)
                        {
                            rowselected++;
                            trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);
                        }
                    }

                    if (rowselected != 1)
                    {
                        MessageUserControl.ShowInfo("Warning", "Select one track to be moved");
                    }
                    else
                    {
                        if (tracknumber == 1)
                        {
                            MessageUserControl.ShowInfo("Warning", "Track cannot be moved up");
                        }
                        else
                        {
                            MoveTrack(trackid, tracknumber, "up");
                        }
                    }
                }
            }

        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            string username = "HansenB";
            //call BLL to move track
            MessageUserControl.TryRun(() =>
            {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(username, PlaylistName.Text, trackid, tracknumber, direction);
                List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                PlayList.DataSource = info;
                PlayList.DataBind();
            },"Moving track","Track has been moved");
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            string username = "HansenB";
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter a playlist name");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Missing Data", 
                        "Play list has no tracks to remove.");
                }
                else
                {
                    //gathering data
                    List<int> trackstodelete = new List<int>();
                    int rowsSelected = 0;
                    CheckBox playlistSelection = null;
                    for(int i = 0; i < PlayList.Rows.Count; i++)
                    {
                        playlistSelection = PlayList.Rows[i].FindControl("Selected") as CheckBox;
                        if (playlistSelection.Checked)
                        {
                            rowsSelected++;
                            trackstodelete.Add(int.Parse((PlayList.Rows[i].FindControl("TrackID") as Label).Text));
                        }
                    }

                    //was a song selected
                    if (rowsSelected == 0)
                    {
                        MessageUserControl.ShowInfo("Missing Data",
                                                "You must select at least one track to remove.");
                    }
                    else
                    {
                        //process delete
                        MessageUserControl.TryRun(() => {
                            PlaylistTracksController sysmgr = new PlaylistTracksController();
                            sysmgr.DeleteTracks(username, PlaylistName.Text, trackstodelete);
                            List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                            PlayList.DataSource = info;
                            PlayList.DataBind();
                        },"Remove Tracks","Tracks have been removed");
                    }

                }
            }
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