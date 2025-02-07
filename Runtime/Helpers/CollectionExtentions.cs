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
            return collection[^1];
        }

        /// <summary>
        /// Shuffles the list, by altering the list directly!
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1); // Random index from 0 to i
                (list[i], list[j]) = (list[j], list[i]); // Swap elements
            }
        }

        /// <summary>
        /// Shuffles the array with the Fisher-Yates shuffle, by altering the list directly!
        /// O(n)
        /// </summary>
        public static void Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            for (int i = n - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1); // Random index from 0 to i
                (array[i], array[j]) = (array[j], array[i]); // Swap elements
            }
        }
    }
}
