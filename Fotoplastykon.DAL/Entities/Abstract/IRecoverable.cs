using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Abstract
{
    public interface IRecoverable
    {
        DateTime? DateDeleted { get; set; }
    }
}
