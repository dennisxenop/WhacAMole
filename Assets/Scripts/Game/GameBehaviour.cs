using Dennis.Events;
using Dennis.Variable;
using Dennis.Variables;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Dennis.Game
{
    public class GameBehaviour : MonoBehaviour, IGameEventListener
    {
        [Header("Events Call")]
        [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
        private Object showStartPlanelObject;

        private ISOAccesableVariable<bool> showStartPanel => showStartPlanelObject as ISOAccesableVariable<bool>;

        [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
        private Object showEnterPlayerNamePanelObject;

        private ISOAccesableVariable<bool> showEnterPlayerNamePanel => showEnterPlayerNamePanelObject as ISOAccesableVariable<bool>;

        [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
        private Object roundRunningObject;

        private ISOAccesableVariable<bool> roundRunning => roundRunningObject as ISOAccesableVariable<bool>;

        [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
        private Object showRoundEndedPanelObject;

        private ISOAccesableVariable<bool> showRoundEndedPanel => showRoundEndedPanelObject as ISOAccesableVariable<bool>;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object roundEndedEventObject;

        private IGameEvent roundEndedEvent => roundEndedEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object gameStartedEventObject;

        private IGameEvent gameStartedEvent => gameStartedEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
        private Object showNewHighScorePanelObject;

        private ISOAccesableVariable<bool> showNewHighScorePanel => showNewHighScorePanelObject as ISOAccesableVariable<bool>;

        [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
        private Object showScorePanelObject;

        private ISOAccesableVariable<bool> showScorePanel => showScorePanelObject as ISOAccesableVariable<bool>;

        [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
        private Object newHighScoreVariableObject;

        private ISOAccesableVariable<bool> newHighScoreVariable => newHighScoreVariableObject as ISOAccesableVariable<bool>;

        [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
        private Object showYourScorePanelObject;

        private ISOAccesableVariable<bool> showYourScorePanel => showYourScorePanelObject as ISOAccesableVariable<bool>;

        [Header("Events Receive")]
        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object startGameButtonEventObject;

        private IGameEvent startGameButtonEvent => startGameButtonEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object playerNameEnteredEventObject;

        private IGameEvent playerNameEnteredEvent => playerNameEnteredEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object tryAgainButtonEventObject;

        private IGameEvent tryAgainButtonEvent => tryAgainButtonEventObject as IGameEvent;

        [SerializeField]
        private FloatVariable timeLeft;

        public void Start()
        {
            StartCoroutine(StartUpGame());
        }

        private IEnumerator StartUpGame()
        {
            yield return new WaitForEndOfFrame();
            gameStartedEvent.Raise();
            showStartPanel.Value = true;

            startGameButtonEvent.RegisterListener(this, () => StartButtonClicked());
            playerNameEnteredEvent.RegisterListener(this, () => PlayerNameEntered());
            tryAgainButtonEvent.RegisterListener(this, () => TryAgainButtonClicked());

            timeLeft.OnValueChanged -= timeLeftChanged;
            timeLeft.OnValueChanged += timeLeftChanged;
        }

        public void OnEnable()
        {
            Assert.IsNotNull(showEnterPlayerNamePanel, "showEnterPlayerNamePanel is not assigned.");
            Assert.IsNotNull(showStartPanel, "startGameEvent is not assigned.");
            Assert.IsNotNull(roundRunning, "roundRunningObject is not assigned.");
            Assert.IsNotNull(showRoundEndedPanel, "showRoundEndedPanel is not assigned.");
            Assert.IsNotNull(gameStartedEvent, "gameStarted is not assigned.");
            Assert.IsNotNull(showNewHighScorePanel, "showNewHighScorePanel is not assigned.");
            Assert.IsNotNull(showScorePanel, "showScorePanel is not assigned.");
            Assert.IsNotNull(newHighScoreVariable, "newHighScoreVariable is not assigned.");
            Assert.IsNotNull(startGameButtonEvent, "startGamePressed is not assigned.");
            Assert.IsNotNull(timeLeft, "timeLeft is not assigned.");
            Assert.IsNotNull(roundEndedEvent, "roundEndedEvent is not assigned.");
            Assert.IsNotNull(playerNameEnteredEvent, "playerNameEnteredEvent is not assigned.");
            Assert.IsNotNull(tryAgainButtonEventObject, "tryAgainButtonEventObject is not assigned.");
            Assert.IsNotNull(showYourScorePanel, "showYourScorePanel is not assigned.");
        }

        private void TryAgainButtonClicked()
        {
            SceneManager.LoadScene(0);
        }

        private void timeLeftChanged()
        {
            if (timeLeft.Value <= 0)
            {
                StartCoroutine(RoundEnded());
            }
        }

        private IEnumerator RoundEnded()
        {
            roundRunning.Value = false;
            roundEndedEvent.Raise();
            showRoundEndedPanel.Value = true;

            yield return new WaitForSeconds(3);

            showRoundEndedPanel.Value = false;
            showYourScorePanel.Value = true;

            if (newHighScoreVariable.Value)
            {
                showNewHighScorePanel.Value = true;
            }

            yield return new WaitForSeconds(3);

            showYourScorePanel.Value = false;
            showNewHighScorePanel.Value = false;
            showScorePanel.Value = true;
        }

        public void OnDisable()
        {
            startGameButtonEvent.UnregisterListener(this, () => StartButtonClicked());
            playerNameEnteredEvent.UnregisterListener(this, () => PlayerNameEntered());
            tryAgainButtonEvent.UnregisterListener(this, () => TryAgainButtonClicked());

            timeLeft.OnValueChanged -= timeLeftChanged;
        }

        private void StartButtonClicked()
        {
            showStartPanel.Value = false;
            showEnterPlayerNamePanel.Value = true;
        }

        private void PlayerNameEntered()
        {
            showEnterPlayerNamePanel.Value = false;
            roundRunning.Value = true;
        }
    }
}