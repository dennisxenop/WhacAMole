using Dennis.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Dennis.Events
{
    public class GameEventBehaviour : MonoBehaviour, IGameEventListener
    {
        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object gameEventObject;

        private IGameEvent gameEvent => gameEventObject as IGameEvent;

        [SerializeField]
        private UnityEvent respone;

        [SerializeField]
        private bool subscribeWhenDeactived;

        private bool subscribed;

        public GameEventBehaviour()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMethod)
        {
            if(subscribeWhenDeactived && this != null && !subscribed) {
                Subscribe();
            }
        }

        private void Subscribe()
        {
            gameEvent.RegisterListener(this, () => respone.Invoke());
            subscribed = true;
        }

        public void OnEnable()
        {
            if(subscribeWhenDeactived) return;
            gameEvent.RegisterListener(this, () => respone.Invoke());
        }

        public void OnDisable()
        {
            if(subscribeWhenDeactived) return;
            gameEvent.UnregisterListener(this, () => respone.Invoke());
        }

        public void OnDestroy()
        {
            gameEvent.UnregisterListener(this, () => respone.Invoke());
        }
    }
}