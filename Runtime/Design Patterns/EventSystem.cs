using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.DesignPatterns
{
    /// <summary>
    /// Event delegate that returns an array of arguments. 
    /// This ensures that events can return any number and type of arguments.
    /// </summary>
    public delegate void EventCallback(object[] _arguments);

    /// <summary>
    /// Global EventSystem class, which acts as a mediator for important events, like dying or completing a level.
    /// It is recommended to make an EventName enum, rather than using ints to identify the event.
    /// </summary>
    public static class EventSystem
    {
        private static Dictionary<int, List<EventCallback>> eventRegister = new Dictionary<int, List<EventCallback>>();

        public static void Subscribe(int _evt, EventCallback _func)
        {
            if (!eventRegister.ContainsKey(_evt))
                eventRegister[_evt] = new List<EventCallback>();

            eventRegister[_evt].Add(_func);
        }

        public static void Unsubscribe(int _evt, EventCallback _func = null)
        {
            if (eventRegister.ContainsKey(_evt))
            {
                var listeners = eventRegister[_evt];
                if (listeners.Count == 1)
                    listeners.Clear();
                else if (_func != null)
                    listeners.Remove(_func);
                else
                    Debug.LogError($"EventSystem: No _func argument was specified, but multiple listeners where found!");
            }
            else
                Debug.LogWarning($"EventSystem: Unsubscribing to event {_evt} failed, no event was registered.");
        }

        public static void RaiseEvent(int _evt, object[] arguments = null)
        {
            if (eventRegister.ContainsKey(_evt))
                foreach (EventCallback e in eventRegister[_evt])
                    e.Invoke(arguments);
        }
    }
} 

