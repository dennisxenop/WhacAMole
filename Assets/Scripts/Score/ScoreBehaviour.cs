using Dennis.Events;
using Dennis.Variables;
using System;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour, IGameEventListener
{
    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private UnityEngine.Object whackGoodEventObject;
    private IGameEvent whackGoodEvent => whackGoodEventObject as IGameEvent;

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private UnityEngine.Object whackWrongEventObject;
    private IGameEvent whackWrongEvent => whackWrongEventObject as IGameEvent;

    [SerializeField]
    private IntVariable scoreVariable;

    public void OnEnable()
    {
        whackGoodEvent.RegisterListener(this, () => WhackGoodEvent());
        whackWrongEvent.RegisterListener(this, () => WhackWrongEvent());
    }

    public void OnDisable()
    {
        whackGoodEvent.UnregisterListener(this, () => WhackGoodEvent());
        whackWrongEvent.UnregisterListener(this, () => WhackWrongEvent());
    }

    private void WhackGoodEvent()
    {
        scoreVariable.Value += 1;
    }
    private void WhackWrongEvent()
    {
        scoreVariable.Value -= 1;
    }
    public void OnEventRaised(Action EventAction)
    {
        EventAction();
    }
}
