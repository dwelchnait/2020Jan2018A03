using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Data.Entity;
using ChinookSystem.Data.Entities;
#endregion

namespace ChinookSystem.DAL
{
    internal class ChinookContext:DbContext
    {
        //the constructor is used to pass the connection string name
        //  to the DbContext class
        public ChinookContext():base("ChinookDB")
        {

        }

        // setup the properties for the DbSet<> used
        //   to access the sql data
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }

        //DTOs and POCOs DO NOT get DbSet<> properties
    }
}
