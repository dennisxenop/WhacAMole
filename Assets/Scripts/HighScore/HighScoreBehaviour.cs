using Dennis.Variables;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

public class HighScoreBehaviour : MonoBehaviour
{
    private string path;

    [SerializeField]
    private IntVariable currentScore;
    [SerializeField]
    private IntVariable highScore;

    private void OnEnable()
    {
        Assert.IsNotNull(currentScore, "currentScore is not assigned.");
        Assert.IsNotNull(highScore, "highScore is not assigned.");

        path = Path.Combine(Application.persistentDataPath, "highScore.txt");

        currentScore.OnValueChanged -= CurrentScoreChanged;
        currentScore.OnValueChanged += CurrentScoreChanged;

        UpdateLocalHighScoreVariable(GetHighScore());
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
        if (GetHighScore() < currentScore.Value)
        {
            SetNewHighScore(currentScore.Value);
        }
    }

    private void SetNewHighScore(int newScore)
    {
        SaveHighScore(newScore);
        UpdateLocalHighScoreVariable(newScore);
    }

    private void UpdateLocalHighScoreVariable(int newScore)
    {
        highScore.Value = newScore;
    }

    public void SaveHighScore(int score)
    {
        File.WriteAllText(path, score.ToString());
    }

    public int GetHighScore()
    {
        if (File.Exists(path))
        {
            string scoreString = File.ReadAllText(path);
            if (int.TryParse(scoreString, out int score))
            {
                return score;
            }
        }

        SaveHighScore(0);
        Debug.LogWarning("Could not retrieve highscore or file does not exist");
        return 0;
    }
}