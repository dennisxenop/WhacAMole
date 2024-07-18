using TMPro;
using UnityEngine;

namespace Dennis.Score
{
    public class ScoreListEntryUIBehaviour : MonoBehaviour, IScoreEntry
    {
        [SerializeField]
        private TextMeshProUGUI scoreNameText;

        [SerializeField]
        private TextMeshProUGUI scoreText;

        [SerializeField]
        private Animation animationComponent;

        public void HighlightScore()
        {
            animationComponent.Play();
        }

        public void Setup(string name, int score)
        {
            animationComponent.Stop();

            scoreNameText.text = name;
            scoreText.text = score.ToString();
        }
    }
}