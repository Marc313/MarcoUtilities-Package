using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.Extensions
{
    /// <summary>
    /// Contains a collection of extension methods for floats, vector3, transforms, gameobjects and meshes.
    /// </summary>
    public static class CollectionExtentions
    {
        /// <summary> Returns a random entry from the collection. </summary>
        public static T GetRandomEntry<T>(this IReadOnlyList<T> collection)
        {
            if (collection == null || collection.Count == 0)
                Debug.LogError($"{collection.GetType()}.GetRandomEntry: Collection was null or empty!");
            return collection[Random.Range(0, collection.Count)];
        }

        /// <summary>
        /// For the real lazy fuckers like me. Returns the last entry of a collection.
        /// Throws an error when the collection is null or empty.
        /// </summary>
        public static T GetLastEntry<T>(this IReadOnlyList<T> collection)
        {
            if (collection == null || collection.Count == 0)
                Debug.LogError($"{collection.GetType()}.GetLastEntry: Collection was null or empty!");
            return collection[collection.Count - 1];
        }

        /// <summary> Returns a random entry from the collection. </summary>
        public static T GetRandomEntry<T>(this T[] collection)
        {
            if (collection == null || collection.Length == 0)
                Debug.LogError($"{collection.GetType()}.GetRandomEntry: Collection was null or empty!");
            return collection[Random.Range(0, collection.Length)];
        }

        /// <summary>
        /// For the real lazy fuckers like me. Returns the last entry of a collection.
        /// Throws an error when the collection is null or empty.
        /// </summary>
        public static T GetLastEntry<T>(this T[] collection)
        {
            if (collection == null || collection.Length == 0)
                Debug.LogError($"{collection.GetType()}.GetLastEntry: Collection was null or empty!");
            return collection[collection.Length - 1];
        }
    }
}
