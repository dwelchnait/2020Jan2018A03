<Query Kind="Expression">
  <Connection>
    <ID>571d9057-778e-4915-a3f7-18a4f076a644</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Grouping

//Basically, grouping is the technique of placing a large pile of data
//   into smaller piles of data depending on a criteria

//navigational properties allow for natural grouping of parent to child (pkey/fkey)
//   collections
//ex     tablerowinstance.childnavproperty.Count() counts all the child
//        records associated with the parent instance

//problem: What if there is no navigational property for the grouping of the
//            data collection?

//here you can use the group clause to create a set of smaller collections based
//    on the desired criteria

//It is important to remember that once the smaller groups are create, ALL reporting
//    MUST use the smaller groups as the collection reference

//report albums by ReleaseYear
from x in Albums
group x by x.ReleaseYear into yearGroups
select yearGroups

//parts of the group
// key component
// instance component

//the key component is created by the "by" criteria
//the "by" criteria can be a) a single attribute; b) a class; c) a list of attributes

//where and orderby clauses can be executed against the group key component or group field
//you can filter(where) or order before grouping
//orderby before grouping is basically useless

//report albums by ReleaseYear showing the year and number of 
//   albums for that year. Order by the year with the most
//   albums, then by the year.

from x in Albums
group x by x.ReleaseYear into yearGroup
orderby yearGroup.Count() descending, yearGroup.Key ascending
select new
{
	year = yearGroup.Key,
	albumcount = yearGroup.Count()
}

//report albums by ReleaseYear showing the year and number of 
//   albums for that year. Order by the year with the most
//   albums, then by the year. Report the album title, artist name,
//   and number of album tracks. Report ONLY albums of the 90s

from x in Albums
//where x.ReleaseYear > 1989 && x.ReleaseYear < 2000
group x by x.ReleaseYear into yearGroup
where yearGroup.Key > 1989 && yearGroup.Key < 2000
orderby yearGroup.Count() descending, yearGroup.Key ascending
select new
{
	year = yearGroup.Key,
	albumcount = yearGroup.Count(),
	albumandartist = from gr in yearGroup
						select new
						{
							title = gr.Title,
							artist = gr.Artist.Name,
							trackcount = gr.Tracks.Count(trk => trk.AlbumId == gr.AlbumId)
						}
}

//note commenting in LinqPad
//  comment ctrl + K + C
//  uncomment ctrl + K + U

//grouping can be done on entity attributes determind using a navigational property
//List tracks for albums produced after 2010 by Genre name. Count tracks for the Name.
from trk in Tracks
where trk.Album.ReleaseYear > 2010
group trk by trk.Genre.Name into gTemp
//select gTemp
select new
{
	genre = gTemp.Key,
	numberof = gTemp.Count()
}

//same report but using the entity as the group criteria
//when you have multiple attributes in a group key
//   you MUST reference the attribute using key.attribute

from trk in Tracks
where trk.Album.ReleaseYear > 2010
group trk by trk.Genre into gTemp
orderby gTemp.Key.Name
//select gTemp
select new
{
	genre = gTemp.Key.Name,
	numberof = gTemp.Count()
}

//Using Group techniques, create a list of customers by employee support individual
//showing the employee lastname, firstname (phone), the number
//of customers for this employee, and a customer list for the
//employee by state, city and customer name (last, first).

//why used the entire entity?
//the entity will become the Key with multiple attributes
//employee info will come from the Key
from c in Customers
group c by c.SupportRepIdEmployee into gTemp
select new
{
	employee = gTemp.Key.LastName + ", " + gTemp.Key.FirstName + "(" + gTemp.Key.Phone + ")",
	customercount = gTemp.Count(),
	customers = from gc in gTemp
				orderby gc.State, gc.City, gc.LastName
				select new
				{
					state = gc.State,
					city = gc.City,
					name = gc.LastName + ", " + gc.FirstName
				}
}

//grouping on multiple attributes NOT a defined class
from c in Customers
group c by new {c.Country, c.State} into gResidence
select gResidence















