using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

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
            Assert.IsNotNull(scoreNameText, "scoreNameText is not found");
            Assert.IsNotNull(scoreText, "scoreText is not found");
            Assert.IsNotNull(animationComponent, "animationComponent is not found");

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