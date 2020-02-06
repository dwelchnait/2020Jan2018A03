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
using ChinookSystem.Data.DTOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class PlaylistController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<MyPlayList> Playlist_GetBySize(int numberoftracks)
        {
            using(var context = new ChinookContext())
            {
                var plresults = from x in context.Playlists
                                where x.PlaylistTracks.Count() == numberoftracks
                                select new MyPlayList
                                {
                                    Name = x.Name,
                                    TrackCount = x.PlaylistTracks.Count(),
                                    PlayTime = x.PlaylistTracks.Sum(plt => plt.Track.Milliseconds),
                                    PlaylistSongs = from y in x.PlaylistTracks
                                                    orderby y.Track.Genre.Name
                                                    select new PlayListSong
                                                    {
                                                        SongName = y.Track.Name,
                                                        Genre = y.Track.Genre.Name
                                                    }
                                };
                return plresults.ToList();
            }
        }

    }
}
