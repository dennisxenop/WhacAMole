using Dennis.Events;
using Dennis.Variables;
using System;
using UnityEngine;
using UnityEngine.Assertions;

public class WhackBehaviour : MonoBehaviour, IGameEventListener
{
    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private UnityEngine.Object whackGoodEventObject;
    private IGameEvent whackGoodEvent => whackGoodEventObject as IGameEvent;

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private UnityEngine.Object whackWrongEventObject;
    private IGameEvent whackWrongEvent => whackWrongEventObject as IGameEvent;

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private UnityEngine.Object AddToScoreEventObject;
    private IGameEvent addToScoreEvent => AddToScoreEventObject as IGameEvent;

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private UnityEngine.Object subtractFromScoreEventObject;
    private IGameEvent subtractFromScoreEvent => subtractFromScoreEventObject as IGameEvent;

    public void OnEnable()
    {
        Assert.IsNotNull(whackGoodEvent, "whackGoodEvent is not assigned.");
        Assert.IsNotNull(whackWrongEvent, "whackWrongEvent is not assigned.");
        Assert.IsNotNull(addToScoreEvent, "addToScoreEvent is not assigned.");
        Assert.IsNotNull(subtractFromScoreEvent, "subtractFromScoreEvent is not assigned.");

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
        addToScoreEvent.Raise();
    }
    private void WhackWrongEvent()
    {
        subtractFromScoreEvent.Raise();
    }
    public void OnEventRaised(Action EventAction)
    {
        EventAction();
    }
}
