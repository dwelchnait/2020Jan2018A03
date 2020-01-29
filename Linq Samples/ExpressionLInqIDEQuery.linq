<Query Kind="Expression">
  <Connection>
    <ID>571d9057-778e-4915-a3f7-18a4f076a644</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//in C# Expression, you must highlight the desired query
//   to execute IF you have more thatn one query in the file

//Query syntax
from x in Albums
select x

//Method syntax
Albums
   .Select (x => x)
   
Artists   

from x in Albums
where x.ArtistId == 51
select x

Albums.Where(x => x.ArtistId == 51).Select(x => x)

Albums
	.Where(x => x.Artist.Name.Contains("A"))
	.Select(x => x)

//list of albums and their artist
//show the album title, release year and artist name
//order by descending year and ascending album title
from x in Albums
orderby x.ReleaseYear descending, x.Title
select new
	{
	AlbumTitle = x.Title,
	Year = x.ReleaseYear,
	ArtistName = x.Artist.Name
	}








