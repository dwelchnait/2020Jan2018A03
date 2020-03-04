using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.DTOs;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
                List<UserPlaylistTrack> results = (from x in context.PlaylistTracks
                                                   where x.Playlist.Name.Equals(playlistname) &&
                                                         x.Playlist.UserName.Equals(username)
                                                   orderby x.TrackNumber
                                                   select new UserPlaylistTrack
                                                   {
                                                       TrackID = x.TrackId,
                                                       TrackNumber = x.TrackNumber,
                                                       TrackName = x.Track.Name,
                                                       Milliseconds = x.Track.Milliseconds,
                                                       UnitPrice = x.Track.UnitPrice
                                                   }).ToList();

                return results;
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //determine if the playlist exists
                //do a query to find the playlist
                //test results == null
                //yes
                //create an instance of playlist
                //load
                //add
                //set tracknumber to 1
                //no
                //query to find max tracknumber
                //tracknumber++
                //query to find track exists
                //test results == null
                //yes
                //throw an exception
                //create an instance of playlisttrack
                //load
                //add
                //SaveChange

                int tracknumber = 0;
                //what would happen if there is no match for the
                //   incoming parameter values
                //we need to ensure that the results have a valid value
                //this value will be resolved by the query either as null
                //  (not found) or an IEnumerable collection
                //we are looking for a single occurance to match the where
                //to achieve a valid value we encapsulate the query in a
                //   (query).FirstOrDefault();
                Playlist exists = (from x in context.Playlists
                             where x.Name.Equals(playlistname)
                               && x.UserName.Equals(username)
                             select x).FirstOrDefault();
                if (exists == null)
                {
                    //new playlist
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    context.Playlists.Add(exists);
                    tracknumber = 1;
                }
                else
                {
                    //existing playlist

                }
            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //code to go here 

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
               //code to go here


            }
        }//eom
    }
}
