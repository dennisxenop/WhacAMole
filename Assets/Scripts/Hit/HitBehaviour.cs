using Dennis.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace Dennis.Hit
{
    public class HitBehaviour : MonoBehaviour, IGameEventListener
    {
        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object hitGoodEventObject;

        private IGameEvent hitGoodEvent => hitGoodEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object hitBadEventObject;

        private IGameEvent hitBadEvent => hitBadEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object AddToScoreEventObject;

        private IGameEvent addToScoreEvent => AddToScoreEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object subtractFromScoreEventObject;

        private IGameEvent subtractFromScoreEvent => subtractFromScoreEventObject as IGameEvent;

        public void OnEnable()
        {
            Assert.IsNotNull(hitGoodEvent, "hitGoodEvent is not assigned.");
            Assert.IsNotNull(hitBadEvent, "hitBadEvent is not assigned.");
            Assert.IsNotNull(addToScoreEvent, "addToScoreEvent is not assigned.");
            Assert.IsNotNull(subtractFromScoreEvent, "subtractFromScoreEvent is not assigned.");

            hitGoodEvent.RegisterListener(this, () => HitGoodEvent());
            hitBadEvent.RegisterListener(this, () => HitBadEvent());
        }

        public void OnDisable()
        {
            hitGoodEvent.UnregisterListener(this, () => HitGoodEvent());
            hitBadEvent.UnregisterListener(this, () => HitBadEvent());
        }

        private void HitGoodEvent()
        {
            addToScoreEvent.Raise();
        }

        private void HitBadEvent()
        {
            subtractFromScoreEvent.Raise();
        }
    }
}