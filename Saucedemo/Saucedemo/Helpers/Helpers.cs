using System.Collections.Generic;

namespace Saucedemo.Helpers
{
    public static class Helpers
    {
        public static Dictionary<Sort, string> SortType { get; } = new Dictionary<Sort, string>()
        {
            {
                Sort.AtoZ, "az"
            },
            {
                Sort.ZtoA, "za"
            },
            {
                Sort.LowToHigh, "lohi"
            },
            {
                Sort.HighToLow, "hilo"
            }
        };

        public enum Sort
        {
            AtoZ,
            ZtoA,
            LowToHigh,
            HighToLow
        }
    }
}
