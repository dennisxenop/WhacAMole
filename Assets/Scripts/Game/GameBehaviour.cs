using Dennis.Events;
using Dennis.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameBehaviour : MonoBehaviour, IGameEventListener
{
    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object startGameEventObject;
    private IGameEvent startGameEvent => startGameEventObject as IGameEvent;


    [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
    private Object roundRunningObject;
    private ISOAccesableVariable<bool> roundRunning => roundRunningObject as ISOAccesableVariable<bool>;

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object roundEndedObject;
    private IGameEvent roundEnded => roundEndedObject as IGameEvent;

    [SerializeField]
    private FloatVariable timeLeft;

    public void OnEnable()
    {
        Assert.IsNotNull(startGameEvent, "startGameEvent is not assigned.");
        Assert.IsNotNull(roundRunningObject, "roundRunningObject is not assigned.");
        Assert.IsNotNull(roundEnded, "roundEnded is not assigned.");


        startGameEvent.RegisterListener(this, () => StartGameButtonEvent());

        timeLeft.OnValueChanged -= timeLeftChanged;
        timeLeft.OnValueChanged += timeLeftChanged;
    }

    private void timeLeftChanged()
    {
        if(timeLeft.Value <= 0) {
            roundRunning.Value = false;
            roundEnded.Raise();
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
