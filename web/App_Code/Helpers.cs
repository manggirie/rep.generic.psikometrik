namespace web.sph.App_Code
{

    public static class Helpers
    {
        /// <summary>
        /// Helper method to determine if a value is between another 2 values
        /// </summary>
        /// <param name="val">The value to evaluate</param>
        /// <param name="min">The floor</param>
        /// <param name="max">The ceiling</param>
        /// <param name="inclusiveMin">default to true, wether to return true if the value is equal to min</param>
        /// <param name="incluseMax">default to true, wether to return true if the value is equal to max</param>
        /// <returns></returns>
        public static bool IsBetween(this int val, int min, int max, bool inclusiveMin = true, bool incluseMax = true)
        {
            if (inclusiveMin && incluseMax)
                return min <= val && val <= max;
            if (inclusiveMin)
                return min <= val && val < max;
            if (incluseMax)
                return min < val && val <= max;

            return false;
        }
    }
}
