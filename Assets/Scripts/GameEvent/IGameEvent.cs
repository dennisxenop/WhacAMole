using System;
using System.Diagnostics.Tracing;

namespace Dennis.Events
{
    public interface IGameEvent
    {
        public void Raise();
        public void RegisterListener(IGameEventListener listener, Action value);
        public void UnregisterListener(IGameEventListener listener, Action value);
    }
}