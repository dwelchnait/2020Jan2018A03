using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;
using DMIT2018Common.UserControls;
using ChinookSystem.Data.POCOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<TrackList> Track_GetTracksByTrackandID(string tracksby, string searcharg)
        {
            using (var context = new ChinookContext())
            {
                List<TrackList> results = null;
                int searchint = 0;
                if (int.TryParse(searcharg, out searchint))
                {
                    //searches for MediaType and Genre
                    results = (from x in context.Tracks
                               where (x.GenreId == searchint && tracksby.Equals("Genre"))
                                 || (x.MediaTypeId == searchint && tracksby.Equals("MediaType"))
                               select new TrackList
                               {
                                   TrackID = x.TrackId,
                                   Name = x.Name,
                                   Title = x.Album.Title,
                                   ArtistName = x.Album.Artist.Name,
                                   MediaName = x.MediaType.Name,
                                   GenreName = x.Genre.Name,
                                   Composer = x.Composer,
                                   Milliseconds = x.Milliseconds,
                                   Bytes = x.Bytes,
                                   UnitPrice = x.UnitPrice
                               }).ToList();
                }
                else
                {
                    //searches for Artist and Album
                    results = (from x in context.Tracks
                               where (x.Album.Title.Contains(searcharg) && tracksby.Equals("Album"))
                                 || (x.Album.Artist.Name.Contains(searcharg) && tracksby.Equals("Artist"))
                               select new TrackList
                               {
                                   TrackID = x.TrackId,
                                   Name = x.Name,
                                   Title = x.Album.Title,
                                   ArtistName = x.Album.Artist.Name,
                                   MediaName = x.MediaType.Name,
                                   GenreName = x.Genre.Name,
                                   Composer = x.Composer,
                                   Milliseconds = x.Milliseconds,
                                   Bytes = x.Bytes,
                                   UnitPrice = x.UnitPrice
                               }).ToList();
                }
                return results;
            }

        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Track_Find(int trackid)
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.Find(trackid);
            }
        }
    }
}
