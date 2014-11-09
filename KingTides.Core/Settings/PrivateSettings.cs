using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingTides.Core.Settings
{
    public class PrivateSettings
    {
        public string Endpoint { get; set; }
        public string MapApplicationId { get; set; }
        public string MapAuthenticationToken { get; set; }

        public static PrivateSettings Default { get; set; }
    }
}
