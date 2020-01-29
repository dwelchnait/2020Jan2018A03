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
	var queryResults = from x in Albums
				orderby x.ReleaseYear descending, x.Title
				select new AlbumArtists
					{
					AlbumTitle = x.Title,
					Year = x.ReleaseYear,
					ArtistName = x.Artist.Name
					};
	//in the C# Statement ide, to view your results
	//  you need to dump your variable
	//the method .Dump() is a LinqPad method
	queryResults.Dump();
}

// Define other methods and classes here
public class AlbumArtists
{
	public string AlbumTitle{get;set;}
	public int Year {get;set;}
	public string ArtistName {get;set;}
}











