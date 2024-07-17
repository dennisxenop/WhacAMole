using Dennis.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameEventBehaviour : MonoBehaviour, IGameEventListener
{
    public GameEventBehaviour()
    {
        SceneManager.sceneLoaded -= Register;
        SceneManager.sceneLoaded += Register;
    }

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object gameEventObject;
    private IGameEvent gameEvent => gameEventObject as IGameEvent;

    [SerializeField]
    private UnityEvent respone;

    [SerializeField, Tooltip("This Game Event Behaviour will only be registerd when enabled")]
    private bool registerOnEnable;

    private void Register(Scene arg0, LoadSceneMode arg1)
    {
        if(this != null) {
            if(registerOnEnable) return;
            gameEvent.RegisterListener(this, () => respone.Invoke());

        }
    }
    public void OnEnable()
    {
        if(!registerOnEnable) return;
        gameEvent.RegisterListener(this, () => respone.Invoke());
    }

    public void OnDisable()
    {
        if(!registerOnEnable) return;
        gameEvent.UnregisterListener(this, () => respone.Invoke());
    }

    public void OnDestroy()
    {
        gameEvent.UnregisterListener(this, () => respone.Invoke());
    }
}
