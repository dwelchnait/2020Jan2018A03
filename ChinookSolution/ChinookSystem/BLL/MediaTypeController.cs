using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.DTOs;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class MediaTypeController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SelectionList> List_MediaTypeNames()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.MediaTypes
                              orderby x.Name
                              select new SelectionList
                              {
                                  IDValueField = x.MediaTypeId,
                                  DisplayText = x.Name
                              };
                return results.ToList();
            }
        }
    }
}
