using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fotoplastykon.API.Areas.Public.Models.Informations
{
    public class InformationModel
    {
        public long Id { get; set; }
        public string CreatedByName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string Content { get; set; }
    }
}
