<Query Kind="Statements">
  <Connection>
    <ID>2679fc39-7e05-4ba2-bc87-a4b797ce2943</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

var results = from x in Albums
			  where x.Artist.Name.Contains("Deep")
			  select new 
			  {
			  	AlbumTitle = x.Title,
				Year = x.ReleaseYear,
				ArtistName = x.Artist.Name
			  };
//the method .Dump() is a LinqPad method only
results.Dump();