using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson_2
{
    internal static class MathConstants {
        public const double PI = 3.14159;
        public const double E = 2.71828;

        public static double CalculateCicumfrance(double radius) {
            return 2 * PI * radius;
        }
        public static double CalculateExponantial(double baceValue)
        {
            return Math.Pow(baceValue, E);
        }
    }
    
}
