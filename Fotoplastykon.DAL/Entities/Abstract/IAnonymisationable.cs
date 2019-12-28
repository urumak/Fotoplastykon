using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Abstract
{
    public interface IAnonymisationable
    {
        DateTime? AnonimisationDate { get; set; }
    }
}
