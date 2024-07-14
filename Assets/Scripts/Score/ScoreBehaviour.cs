using Dennis.Events;
using Dennis.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
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
    private FloatVariable scoreVariable;

    public void OnEnable()
    {
        whackGoodEvent.RegisterListener(this, () => WhackGoodEvent());
        whackWrongEvent.RegisterListener(this, () => WhackWrongEvent());
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
