using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.General
{
    /// <summary>
    /// A Blackboard is a way to pass references and variables across classes,
    /// and is mostly used in this project by owners of a behaviour tree. 
    /// This ensures that the owner of the tree does not have to pass the same references into multiple nodes.
    /// </summary>
    public class Blackboard
    {
        private Dictionary<string, object> SharedVariables = new();

        public void AddOrSet(string key, object value)
        {
            SharedVariables[key] = value;
        }

        public T Read<T>(string key)
        {
            if (SharedVariables.ContainsKey(key))
                Debug.LogError($"BlackBoard: Tried to read key {key}, but key was not found!");

            return (T)SharedVariables[key];
        }

        public void Remove(string key)
        {
            if (SharedVariables.ContainsKey(key))
                Debug.LogError($"BlackBoard: Tried to remove key {key}, but key was not found!");

            SharedVariables.Remove(key);
        }
    }


    /// <summary>
    /// Blackboard that is statically available. 
    /// Important info can be stored in here, or scripts can register themselves, similarly to a ServiceLocator.
    /// </summary>
    public static class GlobalBlackboard
    {
        private static Blackboard blackboard;

        public static void Register(string key, object value) => blackboard.AddOrSet(key, value);
        public static T Read<T>(string key) => blackboard.Read<T>(key);
        public static void Remove(string key) => blackboard.Remove(key);
    }
}
