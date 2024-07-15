using Dennis.Events;
using Dennis.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour, IGameEventListener
{
    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object startGameEventObject;
    private IGameEvent startGameEvent => startGameEventObject as IGameEvent;


    [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
    private Object roundRunningObject;
    private ISOAccesableVariable<bool> roundRunning => roundRunningObject as ISOAccesableVariable<bool>;

    [SerializeField]
    private FloatVariable timeLeft;

    public void OnEnable()
    {
        startGameEvent.RegisterListener(this, () => StartGameButtonEvent());

        timeLeft.OnValueChanged -= timeLeftChanged;
        timeLeft.OnValueChanged += timeLeftChanged;
    }

    private void timeLeftChanged()
    {
        if (timeLeft.Value <= 0)
        {
            roundRunning.Value = false;
        }
    }

    public void OnDisable()
    {
        startGameEvent.UnregisterListener(this, () => StartGameButtonEvent());
        timeLeft.OnValueChanged -= timeLeftChanged;

    }

    public void OnEventRaised(System.Action EventAction)
    {
        EventAction();
    }

    private void StartGameButtonEvent()
    {
        roundRunning.Value = true;
    }
}
