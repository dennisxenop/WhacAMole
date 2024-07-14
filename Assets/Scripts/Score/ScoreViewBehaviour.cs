using Dennis.Variables;
using TMPro;
using UnityEngine;

public class ScoreViewBehaviour : MonoBehaviour
{
    [SerializeField]
    private FloatVariable currentScore;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        currentScore.OnValueChanged -= CurrentScoreChanged;
        currentScore.OnValueChanged += CurrentScoreChanged;
    }

    private void CurrentScoreChanged()
    {
        if (currentScore.Value < 0) return;
        scoreText.text = currentScore.Value.ToString();
    }

    private void Unsubscribe()
    {
        currentScore.OnValueChanged -= CurrentScoreChanged;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }
}
