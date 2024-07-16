using Dennis.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

namespace Dennis.HighScore
{
    public class HighScoreBehaviour : MonoBehaviour
    {
        private string path;

        [SerializeField]
        private IntVariable currentScore;
        [SerializeField]
        private IntVariable highScore;

        [SerializeField]
        private ScoreListVariable scores;

        [SerializeField]
        private StringVariable playerName;

        private void OnEnable()
        {
            Assert.IsNotNull(currentScore, "currentScore is not assigned.");
            Assert.IsNotNull(highScore, "highScore is not assigned.");

            path = Path.Combine(Application.persistentDataPath, "highScore.txt");

            currentScore.OnValueChanged -= CurrentScoreChanged;
            currentScore.OnValueChanged += CurrentScoreChanged;

            scores.AddRange(LoadScores());

            if (scores.Count > 0)
            {
                UpdateLocalHighScoreVariable(scores[0].Score);
            }
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
            if (scores[0].Score < currentScore.Value)
            {
                UpdateLocalHighScoreVariable(currentScore.Value);
            }
        }

        private void UpdateLocalHighScoreVariable(int newScore)
        {
            highScore.Value = newScore;
        }

        public void SaveScores(List<HighScoreEntry> highScores)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (HighScoreEntry entry in highScores)
                {
                    writer.WriteLine(entry.Name + "," + entry.Score);
                }
            }
        }

        public List<HighScoreEntry> LoadScores()
        {
            List<HighScoreEntry> highScores = new List<HighScoreEntry>();

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            HighScoreEntry entry = new HighScoreEntry(parts[0], int.Parse(parts[1]));
                            highScores.Add(entry);
                        }
                    }
                }
            }

            return highScores;
        }
    }
}