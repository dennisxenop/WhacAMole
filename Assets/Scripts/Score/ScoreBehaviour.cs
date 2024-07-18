using Dennis.Events;
using Dennis.Variables;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

namespace Dennis.Score
{
    public class ScoreBehaviour : MonoBehaviour, IGameEventListener
    {
        private string path;

        [SerializeField]
        private ScoreVariable currentScore;

        [SerializeField]
        private ScoreVariable highScore;

        [SerializeField]
        private ScoreListVariable scores;

        [SerializeField]
        private StringVariable playerName;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object roundEndedObject;

        private IGameEvent roundEndedEvent => roundEndedObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private UnityEngine.Object AddToScoreEventObject;

        private IGameEvent addToScoreEvent => AddToScoreEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private UnityEngine.Object subtractFromScoreEventObject;

        private IGameEvent subtractFromScoreEvent => subtractFromScoreEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(IGameEvent))]
        private Object playerNameEnteredEventObject;

        private IGameEvent playerNameEnteredEvent => playerNameEnteredEventObject as IGameEvent;

        [SerializeField, RequireInterface(typeof(ISOAccesableVariable<bool>))]
        private UnityEngine.Object newHighScoreVariableObject;

        private ISOAccesableVariable<bool> newHighScoreVariable => newHighScoreVariableObject as ISOAccesableVariable<bool>;

        private void OnEnable()
        {
            Assert.IsNotNull(currentScore, "currentScore is not assigned.");
            Assert.IsNotNull(highScore, "highScore is not assigned.");
            Assert.IsNotNull(scores, "scores is not assigned.");
            Assert.IsNotNull(playerName, "playerName is not assigned.");
            Assert.IsNotNull(roundEndedEvent, "roundEnded is not assigned.");
            Assert.IsNotNull(addToScoreEvent, "roundEnded is not assigned.");
            Assert.IsNotNull(subtractFromScoreEvent, "roundEnded is not assigned.");
            Assert.IsNotNull(newHighScoreVariable, "newHighScoreVariable is not assigned.");
            Assert.IsNotNull(playerNameEnteredEvent, "playerNameEnteredEvent is not assigned.");

            path = Path.Combine(Application.persistentDataPath, "score.txt");

            currentScore.OnValueChanged -= CurrentScoreChanged;
            currentScore.OnValueChanged += CurrentScoreChanged;

            roundEndedEvent.RegisterListener(this, () => RoundEnded());
            addToScoreEvent.RegisterListener(this, () => AddToScore());
            playerNameEnteredEvent.RegisterListener(this, () => NameEntered());
            subtractFromScoreEvent.RegisterListener(this, () => SubtractScore());

            scores.Set(LoadScores());

            if(scores.Count > 0) {
                UpdateLocalHighScoreVariable(scores[0].Name, scores[0].Score);
            }
        }

        private void NameEntered()
        {
            SetScore(0);
        }

        private void SetScore(int newScore)
        {
            currentScore.Value = new ScoreEntry(playerName.Value, newScore);
        }

        private void SubtractScore()
        {
            int newScore = currentScore.Value.Score - 1;
            SetScore(newScore);
        }

        private void AddToScore()
        {
            int newScore = currentScore.Value.Score + 1;
            SetScore(newScore);
        }

        private void RoundEnded()
        {
            List<ScoreEntry> entries = new List<ScoreEntry>(scores) {
                currentScore.Value
        };
            entries.Sort((x, y) => x.Score.CompareTo(y.Score));
            entries.Reverse();
            scores.Set(entries);
            SaveScores(entries);
        }

        private void OnDisable()
        {
            currentScore.OnValueChanged -= CurrentScoreChanged;
        }

        private void CurrentScoreChanged()
        {
            CheckForHighScore();
        }

        private void CheckForHighScore()
        {
            if(highScore.Value.Score < currentScore.Value.Score) {
                UpdateLocalHighScoreVariable(playerName.Value, currentScore.Value.Score);
                newHighScoreVariable.Value = true;
            }
        }

        private void UpdateLocalHighScoreVariable(string playerName, int newScore)
        {
            highScore.Value = new ScoreEntry(playerName, newScore);
        }

        public void SaveScores(List<ScoreEntry> scores)
        {
            using(StreamWriter writer = new StreamWriter(path)) {
                foreach(ScoreEntry entry in scores) {
                    writer.WriteLine(entry.Name + "," + entry.Score);
                }
            }
        }

        public List<ScoreEntry> LoadScores()
        {
            List<ScoreEntry> highScores = new List<ScoreEntry>();

            if(File.Exists(path)) {
                using(StreamReader reader = new StreamReader(path)) {
                    string line;
                    while((line = reader.ReadLine()) != null) {
                        string[] parts = line.Split(',');
                        if(parts.Length == 2) {
                            ScoreEntry entry = new ScoreEntry(parts[0], int.Parse(parts[1]));
                            highScores.Add(entry);
                        }
                    }
                }
            }

            return highScores;
        }
    }
}