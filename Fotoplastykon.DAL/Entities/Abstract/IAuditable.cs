using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.DAL.Entities.Abstract
{
    public interface IAuditable
    {
        DateTime DateCreated { get; set; }
    }
}
