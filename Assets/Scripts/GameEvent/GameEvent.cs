using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dennis.Events
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject, IGameEvent
    {
        private readonly Dictionary<IGameEventListener, Action> eventListeners =
            new Dictionary<IGameEventListener, Action>();

        public void Raise()
        {
            foreach(KeyValuePair<IGameEventListener, Action> eventListener in eventListeners.Reverse()) {
                eventListener.Key.OnEventRaised(eventListener.Value);
            }
        }

        public void RegisterListener(IGameEventListener listener, Action EventAction)
        {
            if(!eventListeners.ContainsKey(listener))
                eventListeners.Add(listener, EventAction);
        }

        public void UnregisterListener(IGameEventListener listener, Action EventAction)
        {
            if(eventListeners.ContainsKey(listener))
                eventListeners.Remove(listener);
        }
    }
}

