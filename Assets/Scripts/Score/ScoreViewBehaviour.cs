using Dennis.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoreViewBehaviour : MonoBehaviour
{
    [SerializeField]
    private ScoreVariable currentScore;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        Assert.IsNotNull(currentScore, "currentScore is not assigned.");
        Assert.IsNotNull(scoreText, "scoreText is not assigned.");

        currentScore.OnValueChanged -= CurrentScoreChanged;
        currentScore.OnValueChanged += CurrentScoreChanged;
    }

    private void CurrentScoreChanged()
    {
        if(currentScore.Value.Score < 0) return;
        scoreText.text = currentScore.Value.Score.ToString();
    }

    private void OnDisable()
    {
        currentScore.OnValueChanged -= CurrentScoreChanged;
    }
}
