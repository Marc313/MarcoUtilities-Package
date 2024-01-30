using System.Collections.Generic;

namespace MarcoUtilities.DesignPatterns
{

    public delegate void EventCallback(object _value);

    /// <summary>
    /// To refactor.
    /// </summary>
    /// <param name="_value"></param>
    public static class EventSystem
    {
        private static Dictionary<string, List<EventCallback>> eventRegister = new Dictionary<string, List<EventCallback>>();

        public static void Subscribe(string _evt, EventCallback _func)
        {
            if (!eventRegister.ContainsKey(_evt))
            {
                eventRegister[_evt] = new List<EventCallback>();
            }

            eventRegister[_evt].Add(_func);
        }

        public static void Unsubscribe(string _evt, EventCallback _func)
        {
            if (eventRegister.ContainsKey(_evt))
            {
                eventRegister[_evt].Remove(_func);
            }
        }

        public static void RaiseEvent(string _evt, object _value = null)
        {
            if (eventRegister.ContainsKey(_evt))
            {
                foreach (EventCallback e in eventRegister[_evt])
                {
                    e.Invoke(_value);
                }
            }
        }
    }

    //public delegate void EventCallback(object _value);

    /*    public static class EventSystem
        {
            private static Dictionary<EventName, List<Action>> eventRegister = new Dictionary<EventName, List<Action>>();

            public static void Subscribe(EventName _evt, Action _func)
            {
                if (!eventRegister.ContainsKey(_evt))
                {
                    eventRegister[_evt] = new List<Action>();
                }

                eventRegister[_evt].Add(_func);
            }

            public static void Unsubscribe(EventName _evt, Action _func)
            {
                if (eventRegister.ContainsKey(_evt))
                {
                    eventRegister[_evt].Remove(_func);
                }
            }

            public static void RaiseEvent(EventName _evt, object _value = null)
            {
                if (eventRegister.ContainsKey(_evt))
                {
                    foreach (Action e in eventRegister[_evt])
                    {
                        e.Invoke();
                    }
                }
            }
        }*/
} 

