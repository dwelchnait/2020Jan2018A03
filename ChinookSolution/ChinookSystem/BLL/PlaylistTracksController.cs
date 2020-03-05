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
using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        // create a class level private data member to hold your list of errors
        private List<string> errors = new List<string>();
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
                //  yes
                //      create an instance of playlist
                //      load
                //      add
                //      set tracknumber to 1
                //no
                //      query to find track exists
                //      test results == null
                //          yes
                //              throw an exception
                //          no
                //              query to find max tracknumber
                //              tracknumber++
                //create an instance of playlisttrack
                //load
                //add
                //SaveChange

                int tracknumber = 0;
                PlaylistTrack newtrack = null;
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
                    newtrack = (from x in context.PlaylistTracks
                                       where x.Playlist.Name.Equals(playlistname)
                                         && x.Playlist.UserName.Equals(username)
                                         && x.TrackId == trackid
                                       select x).FirstOrDefault();
                    if (newtrack == null)
                    {
                        //not found can be added
                        tracknumber = (from x in context.PlaylistTracks
                                    where x.Playlist.Name.Equals(playlistname)
                                      && x.Playlist.UserName.Equals(username)
                                    select x.TrackNumber).Max();
                        tracknumber++;
                    }
                    else
                    {
                        //found violates business rule
                        //two ways of handling the message
                        //a) single error
                        //b) multiple business rule errors

                        //a)
                        //throw new Exception("Song already exists on playlist. Choose something else.");

                        //b) use the BusinessRuleException class to throw the error
                        //   this technique can be used in your BLL and onyour Web page
                        //   to this technique, you will collect your errors within
                        //       a List<string>; then throw the BusinessRuleException
                        //       along with the List<string> errors
                        //   
                        errors.Add("**Song already exists on playlist. Choose something else.");

                    }
                }

                //finish all possible business rule validation
                if (errors.Count > 0)
                {
                    throw new BusinessRuleException("Adding Track", errors);
                }

                //add the new playlist track record
                newtrack = new PlaylistTrack();
                //when you do an .Add(xxx) to a entity, the record
                //  is ONLY staged AND NOT yet on the database
                //ANY expected pkey value DOES NOT yet exist

                //newtrack.PlaylistId = exists.PlaylistId; removed
                newtrack.TrackId = trackid;
                newtrack.TrackNumber = tracknumber;
                exists.PlaylistTracks.Add(newtrack);

                //SaveChange is what actually affects the database
                context.SaveChanges();
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
