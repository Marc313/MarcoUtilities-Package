using UnityEngine;

namespace MarcoUtilities.Extensions
{
    /// <summary>
    /// Static class that contains helper functions
    /// </summary>
    public static class Helpers
    {
        #region Float Helpers
        // Float Helpers \\

        /// <summary>
        /// Maps a float 'value' from its original range in between 'min1' and 'max1' to a new range between 'min2' and 'max2'
        /// </summary>
        public static float Map(float min1, float max1, float min2, float max2, float value)
        {
            float normalizedValue = Mathf.InverseLerp(min1, max1, value);
            float newValue = Mathf.Lerp(min2, max2, normalizedValue);
            return newValue;
        }
        #endregion
    }
}
