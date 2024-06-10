using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweeper.Enums;

namespace MineSweeper.Enums
{
    public static class Resources
    {
        public static Dictionary<string, object> CardTag { get; } = new Dictionary<string, object>()
        {
            { "locationType", LocationType.empty},
            { "UserInput", UserInput.empty},
        };
    }
}

