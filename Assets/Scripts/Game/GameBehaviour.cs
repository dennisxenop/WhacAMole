using Dennis.Events;
using Dennis.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameBehaviour : MonoBehaviour, IGameEventListener
{
    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object startRoundEventObject;
    private IGameEvent startRoundEvent => startRoundEventObject as IGameEvent;

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object roundStartedEventObject;
    private IGameEvent roundStartedEvent => roundStartedEventObject as IGameEvent;

    [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
    private Object roundRunningObject;
    private ISOAccesableVariable<bool> roundRunning => roundRunningObject as ISOAccesableVariable<bool>;

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object roundEndedObject;
    private IGameEvent roundEnded => roundEndedObject as IGameEvent;

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object gameStartedEventObject;
    private IGameEvent gameStartedEvent => gameStartedEventObject as IGameEvent;

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object showNewHighScorePanelEventObject;
    private IGameEvent showNewHighScorePanelEvent => showNewHighScorePanelEventObject as IGameEvent;

    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object showScorePanelEventObject;
    private IGameEvent showScorePanelEvent => showScorePanelEventObject as IGameEvent;

    [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
    private Object newHighScoreVariableObject;
    private ISOAccesableVariable<bool> newHighScoreVariable => newHighScoreVariableObject as ISOAccesableVariable<bool>;


    [SerializeField]
    private FloatVariable timeLeft;

    public void Start()
    {
        gameStartedEvent.Raise();
    }
    public void OnEnable()
    {
        Assert.IsNotNull(startRoundEvent, "startGameEvent is not assigned.");
        Assert.IsNotNull(roundRunningObject, "roundRunningObject is not assigned.");
        Assert.IsNotNull(roundEnded, "roundEnded is not assigned.");
        Assert.IsNotNull(gameStartedEvent, "gameStartedEvent is not assigned.");
        Assert.IsNotNull(showNewHighScorePanelEvent, "showNewHighScorePanelEvent is not assigned.");
        Assert.IsNotNull(showScorePanelEvent, "showScorePanelEvent is not assigned.");
        Assert.IsNotNull(newHighScoreVariable, "showScorePanelEvent is not assigned.");


        startRoundEvent.RegisterListener(this, () => StartGameButtonEvent());

        timeLeft.OnValueChanged -= timeLeftChanged;
        timeLeft.OnValueChanged += timeLeftChanged;
    }

    private void timeLeftChanged()
    {
        if(timeLeft.Value <= 0) {
            StartCoroutine(RoundEnded());

        }
    }

    private IEnumerator RoundEnded()
    {
        roundRunning.Value = false;
        yield return new WaitForSeconds(1);
        roundEnded.Raise();
        yield return new WaitForSeconds(3);
        if(newHighScoreVariable.Value) {
            showNewHighScorePanelEvent.Raise();
            yield return new WaitForSeconds(3);
        }
        showScorePanelEvent.Raise();
    }


    public void OnDisable()
    {
        startRoundEvent.UnregisterListener(this, () => StartGameButtonEvent());
        timeLeft.OnValueChanged -= timeLeftChanged;

    }

    private void StartGameButtonEvent()
    {
        StartCoroutine(StartRound());
    }

    private IEnumerator StartRound()
    {
        roundStartedEvent.Raise();
        yield return new WaitForEndOfFrame();
        roundRunning.Value = true;
    }
}
