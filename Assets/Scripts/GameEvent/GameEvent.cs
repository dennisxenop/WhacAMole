using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Dennis.Events
{
    public class GameEvent : ScriptableObject, IGameEvent
    {
        private Dictionary<IGameEventListener, List<Action>> eventListeners = new Dictionary<IGameEventListener, List<Action>>();

        public void Raise()
        {
            foreach(KeyValuePair<IGameEventListener, List<Action>> eventListener in eventListeners.Reverse()) {

                Assert.IsNotNull(eventListener.Key, "EventListener key cannot be null.");
                Assert.IsNotNull(eventListener.Value, "EventListener value cannot be null.");

                if(eventListener.Key == null || eventListener.Value == null) {
                    continue;
                }

                foreach(Action eventAction in eventListener.Value) {

                    Assert.IsNotNull(eventAction, "EventAction cannot be null.");

                    if(eventAction == null) {
                        continue;
                    }

                    eventListener.Key.OnEventRaised(eventAction);
                }
            }
        }

        public void RegisterListener(IGameEventListener listener, Action eventAction)
        {
            Assert.IsNotNull(listener, "Listener cannot be null.");
            Assert.IsNotNull(eventAction, "EventAction cannot be null.");

            if(!eventListeners.ContainsKey(listener)) {
                eventListeners[listener] = new List<Action>();
            }

            if(!eventListeners[listener].Contains(eventAction)) {
                eventListeners[listener].Add(eventAction);
            }
        }

        public void UnregisterListener(IGameEventListener listener, Action eventAction)
        {
            Assert.IsNotNull(listener, "Listener cannot be null.");
            Assert.IsNotNull(eventAction, "EventAction cannot be null.");

            if(eventListeners.ContainsKey(listener)) {
                eventListeners[listener].Remove(eventAction);

                if(eventListeners[listener].Count == 0) {
                    eventListeners.Remove(listener);
                }
            }
        }
    }
}