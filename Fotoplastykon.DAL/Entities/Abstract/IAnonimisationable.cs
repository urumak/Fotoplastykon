using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Abstract
{
    public interface IAnonimisationable
    {
        DateTime? AnonimisationDate { get; set; }
    }
}
