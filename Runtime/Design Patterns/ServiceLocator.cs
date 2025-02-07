using System;
using System.Collections.Generic;

namespace MarcoUtilities.DesignPatterns
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> objects = new Dictionary<Type, object>();

        public static void RegisterService<T>(T t)
        {
            if (objects.ContainsKey(typeof(T)))
            {
                objects[typeof(T)] = t;
            }
            else
            {
                objects.Add(typeof(T), t);
            }
        }

        public static bool HasService<T>()
        {
            return objects.ContainsKey(typeof(T));
        }

        public static T GetService<T>()
        {
            return (T)objects[typeof(T)];
        }
    }
}