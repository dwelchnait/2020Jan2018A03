<Query Kind="Program">
  <Connection>
    <ID>571d9057-778e-4915-a3f7-18a4f076a644</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//Nested queries
	//
	//a query within a query
	
	//the query is returned as an IEnumerable<T> or IQueryable<T>
	//if you need to return your query as a List<T> then you must
	//   encapsulate your query and add a .ToList() on the sub query
	//        (from ....).ToList()
	
	//ToList() is also useful if your requeire your data in memory for
	// some execution
	
	//Create a list of albums showing their title and artist.
	//Show only ablums with 25 or more tracks.
	//Show the songs on the album (name and length)
	
	var results = from x in Albums	
					where x.Tracks.Count() >= 25
					select new AlbumList
					{
						AlbumTitle = x.Title,
						ArtistName = x.Artist.Name,
						Songs = (from y in x.Tracks
								select new Song
								{
									SongName = y.Name,
									SongLength = y.Milliseconds
								}).ToList()
					};
//results.Dump();

//Create a list of Playlist with more than 15 tracks.
//Show the playlist name, count of tracks, total play time for 
//the playlist and the list of the tracks.
//For each track show the song name and Genre.
//Use strong datatypes for all data.

var plresults = from x in Playlists
				where x.PlaylistTracks.Count() > 15
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
plresults.Dump();

}

// Define other methods and classes here

// class that contains the album title, album artist, list of tracks (DTO)
public class AlbumList
{
	public string AlbumTitle {get;set;}
	public string ArtistName {get;set;}
	public List<Song> Songs {get;set;}
}

// class that contains the track song name and length (POCO)
public class Song
{
	public string SongName {get;set;}
	public int SongLength {get;set;}
}

public class MyPlayList
{
	public string Name{get;set;}
	public int TrackCount{get;set;}
	public int PlayTime {get;set;}
	public IEnumerable<PlayListSong> PlaylistSongs {get;set;}
}

public class PlayListSong
{
	public string SongName {get;set;}
	public string Genre {get;set;}
}



