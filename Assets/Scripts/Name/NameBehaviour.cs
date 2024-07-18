using Dennis.Events;
using Dennis.Variables;
using UnityEngine;

namespace Demmos.Name
{
    public class NameBehaviour : MonoBehaviour, IGameEventListener
    {
        [Header("Events Receive")]
        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object nameEnteredEventObject;

        private IGameEvent nameEnteredEvent => nameEnteredEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
        private Object showInCorrectNameObject;

        private ISOAccesableVariable<bool> showInCorrectName => showInCorrectNameObject as ISOAccesableVariable<bool>;

        [Header("Events Call")]
        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object nameSuccesfullEnteredObject;

        private IGameEvent nameSuccesfullEntered => nameSuccesfullEnteredObject as IGameEvent;

        [SerializeField]
        private StringVariable playerName;

        public void OnEnable()
        {
            nameEnteredEvent.RegisterListener(this, () => NameEnteredEvent());
        }

        private void NameEnteredEvent()
        {
            if(string.IsNullOrEmpty(playerName.Value)) {
                showInCorrectName.Value = true;
            } else {
                nameSuccesfullEntered.Raise();
            }
        }
    }
}