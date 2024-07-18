using System;

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