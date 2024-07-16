using System;
using UnityEngine;
using UnityEngine.Events;

namespace Dennis.Events
{
    public interface IGameEventListener
    {
        public void OnEventRaised(Action EventAction)
        {
            EventAction();
        }
    }
}

