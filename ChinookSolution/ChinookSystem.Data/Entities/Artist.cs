using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ChinookSystem.Data.Entities
{
    //identify the sql entity (table) this class maps
    [Table("Artists")]
    public class Artist
    {
        //fully implemented properties will be used for nullable strings.
        private string _Name;

        [Key]
        public int ArtistId { get; set; }

        [StringLength(120,ErrorMessage ="Artist name is limited to 120 characters.")]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = string.IsNullOrEmpty(value) ? null : value;
                //if (string.IsNullOrEmpty(value))
                //{
                //    _Name = null;
                //}
                //else
                //{
                //    _Name = value;
                //}
            }
        }

        //virtual navigational properties
        //these properties do not contain data
        //these properties form a virtual relationship
        //    between the entity classes
        //you create the appropriate virtual properties such
        //   as you are mapping the ERD relationship that
        //   exists in your database
        public virtual ICollection<Album> Albums { get; set; }
    }
}
