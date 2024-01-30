using UnityEngine;

namespace MarcoUtilities.Extensions
{
    /// <summary>
    /// Contains a collection of extension methods for floats, vector3, transforms, gameobjects and meshes.
    /// </summary>
    public static class CollectionExtentions
    {
        public static T GetRandomEntry<T>(this T[] _array)
        {
            if (_array == null || _array.Length == 0) return default(T);
            return _array[Random.Range(0, _array.Length)];
        }

        public static T GetLastEntry<T>(this T[] _array)
        {
            if (_array == null || _array.Length == 0) return default(T);
            return _array[_array.Length - 1];
        }
    }
}
