<Query Kind="Statements">
  <Connection>
    <ID>571d9057-778e-4915-a3f7-18a4f076a644</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//distinct data

//List of countries in which we have customers.
var result1 = (from x in Customers
orderby x.Country
select x.Country).Distinct();
result1.Dump();

//boolean filters .All() and .Any()

//.Any() method iterates through the entire collection to see
//   if any of the items match the specified condition
//returns no data just a true or false
//an instance of the collection that receives a true on the
//   condition is selected for processing

//Genres.OrderBy(x => x.Name).Dump();

//Show Genres that have tracks which are not on any playlist
var results2 = from x in Genres
				where x.Tracks.Any(trk => trk.PlaylistTracks.Count() == 0)
				orderby x.Name
				select x.Name;
results2.Dump();

//.All() method iterates through the entire collection to see
//    if all of the items mathc the specific condition
//returns no data just a true or false
//an instance of the collection that receives a true on the condition
//    is selected for processing

//Show Genres that have all their tracks appearing at least once 
//   on a playlist
var populargenres = from x in Genres
					where x.Tracks.All(trk => trk.PlaylistTracks.Count() > 0)
					orderby x.Name
					select new
					{
						genre = x.Name,
						thetracks = from y in x.Tracks
									where y.PlaylistTracks.Count() > 0
									select new
									{
										song = y.Name,
										count = y.PlaylistTracks.Count()
									}
					};
populargenres.Dump();

//Sometimes you have two collections that need to be compare
//Usually you are looking for items that are the same (in both collections)
//OR you are looking for items that are different
//In either case you are comparing one collection to a second collection

//obtain a distinct list of all playlist tracks for Roberto Almeida 
//  username of AlmeidaR
var almeida =(from x in PlaylistTracks
				where x.Playlist.UserName.Contains("Almeida")
				orderby x.Track.Name
				select new
				{
					genre = x.Track.Genre.Name,
					id = x.TrackId,
					song = x.Track.Name
				}).Distinct();
almeida.Dump();

//obtain a distinct list of all playlist tracks for Michelle Brooks 
//  username of BrooksM
var brooks =(from x in PlaylistTracks
				where x.Playlist.UserName.Contains("Brooks")
				orderby x.Track.Name
				select new
				{
					genre = x.Track.Genre.Name,
					id = x.TrackId,
					song = x.Track.Name
				}).Distinct();
brooks.Dump();

//when you are comparing two collections, you need to determine
//which collection will be collection1 and which will be collection2

//Create a List of tracks that both Roberto and Michelle like
var likes = almeida
	.Where(a => brooks.Any(b => b.id == a.id))
	.OrderBy(a => a.genre)
	.Select(a => a)
	.Dump();

//Create a List of tracks that Roberto has but Michelle doesn't
var robertoOnly = almeida
	.Where(a => !brooks.Any(b => b.id == a.id))
	.OrderBy(a => a.genre)
	.Select(a => a)
	.Dump();

//Create a List of tracks that Michelle has but Roberto doesn't
//using .Any()
//note where the ! is placed
var michelleOnlyAny = brooks
	.Where(a => !almeida.Any(b => b.id == a.id))
	.OrderBy(a => a.genre)
	.Select(a => a)
	.Dump();

//using .All()
//note where the ! is placed
var michelleOnlyAll = brooks
	.Where(a => almeida.All(b => b.id != a.id))
	.OrderBy(a => a.genre)
	.Select(a => a)
	.Dump();


//to concatentate two the results from multiple queries
//   you can use the .Union()
//This operates in the same fashion as the sql union command
//rules are quite similar between to two .Union() and union command
//number of columns same
//column datatype same
//ordering done on the last query

//Create a list of Albums showing their title, total track count,
//  total price of tracks, and the Average length of the tracks

//query1 will report albums that have tracks
//query2 will report albums without tracks (there is no tracks to Sum or Average)

//(query1).Union(query2).OrdeBy(first sort).ThenBy(nth sort)

//Average() is returned as a double (Milliseconds is an integer)
//Count() is an integer
//Sum() is a decimal (UnitPrice)
var unionresults = (from x in Albums
					where x.Tracks.Count() > 0
					select new
					{
						title = x.Title,
						trackcount = x.Tracks.Count(),
						trackcost = x.Tracks.Sum(y => y.UnitPrice),
						avglengthA = x.Tracks.Average(y => y.Milliseconds)/1000.0,
						avglengthB = x.Tracks.Average(y => y.Milliseconds/1000.0)
					}).Union(from x in Albums
						where x.Tracks.Count() == 0
						select new
						{
							title = x.Title,
							trackcount = 0,
							trackcost = 0.00m,
							avglengthA = 0.00,
							avglengthB = 0.00
						})
						.OrderBy( y => y.trackcount)
						.ThenBy(y => y.title)
						.Dump();











