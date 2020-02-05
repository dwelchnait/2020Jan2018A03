<Query Kind="Expression">
  <Connection>
    <ID>571d9057-778e-4915-a3f7-18a4f076a644</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Aggregates

//.Count(), .Sum(), .Min(), .Max(), .Average()
//aggregates work against collections (0, 1 or more record datasets)

//list all albums showing the album title, album artist,
//and the number of tracks for the album

// Artists -> Albums -> Tracks
// ICollection(Album) -> ICollections ->... (parent to child)
//  <- Artist <- Album  (child to parent)

//aggregate using method syntax
from x in Albums
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	trackcount = x.Tracks.Count()
}

//aggregate using query syntax and navigational properties
from x in Albums
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	trackcount = (from y in x.Tracks
					select y).Count()
}

//aggregate using query syntax and navigational property for 
// artist BUT NOT for Tracks
//the navigational property down to the Tracks from Albums
//   is replaced with a where clause comparing FKey to PKey
from x in Albums
select new
{
	title = x.Title,
	artist = x.Artist.Name,
	trackcount = (from y in Tracks
					where y.AlbumId == x.AlbumId
					select y).Count()
}

//list the artists and their number of albums
//order the list from most albums by an artist to least albums
from x in Artists
orderby x.Albums.Count() descending, x.Name ascending
select new
{ 
	name = x.Name,
	albums = x.Albums.Count()
}

//find the maximum number of albums for all artist
//1st: a list which represents the count of albums per artist
//2nd: what is the highest number in that list

(Artists.Select(x => x.Albums.Count())).Max()

(from x in Artists
select x.Albums.Count()).Max()

//produce a list of ablums which have tracks showing their
//title, artist name, number of tracks and total price of
//all tracks for that album


from x in Albums
where x.Tracks.Count() > 0
select new
{
	title = x.Title,
	name = x.Artist.Name,
	trackcount = x.Tracks.Count(),
	tracktotal = x.Tracks.Sum(tr => tr.UnitPrice)
}

from x in Albums
where x.Tracks.Count() > 0
select new
{
	title = x.Title,
	name = x.Artist.Name,
	trackcount = (from y in x.Tracks
					select y).Count(),
	tracktotal = (from y in Tracks
					where y.AlbumId == x.AlbumId
					select y.UnitPrice).Sum()
}

//list all the playlist which have a track showing
//the playlist name, number of tracks for the playlist,
//the cost of the playlist, and total storage size
// for the playlist in megabytes.

//Playlists     PlaylistTracks     Tracks
//  .Name							.UnitPrice,.Bytes
//		ICollection			   Track
//			Playlists		ICollection

from x in Playlists
where x.PlaylistTracks.Count()
select new
{
	name = x.Name,
	numberoftracks = x.PlaylistTracks.Count(),
	cost = x.PlaylistTracks.Sum(plt => plt.Track.UnitPrice),
	storage = x.PlaylistTracks.Sum(plt => plt.Track.Bytes/1000000.0)
}















