using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Fotoplastykon.DAL.Enums
{
    public enum RoleType
    {
        [Description("Aktor")]
        Actor,

        [Description("Reżyser")]
        Director,

        [Description("Scenarzysta")]
        Scenarist,

        [Description("Autor zdjęć")]
        Cinematographer,

        [Description("Kompozytor")]
        Composer
    }
}
