using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Dennis.Score
{
    public class ScoreEntryUIBehaviour : MonoBehaviour, IScoreEntry
    {
        [SerializeField]
        private TextMeshProUGUI scoreNameText;
        [SerializeField]
        private TextMeshProUGUI scoreText;
        public void HighlightScore()
        {
        }

        public void Setup(string name, int score)
        {
            scoreNameText.text = name;
            scoreText.text = score.ToString();
        }
    }
}