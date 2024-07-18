using Dennis.Variables;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Dennis.Score
{
    public class ScoreListUIBehaviour : MonoBehaviour
    {
        [SerializeField]
        private ScoreListVariable scoreListVariable;

        [SerializeField]
        private Transform contentParent;

        [SerializeField]
        private GameObject scoreEntryUIPrefabObject;

        private List<IScoreEntry> instantiedScoreEntrys = new List<IScoreEntry>();

        [SerializeField]
        private ScoreVariable currentScore;

        public void OnEnable()
        {
            Assert.IsNotNull(scoreListVariable, "scoreListVariable is not found");
            Assert.IsNotNull(contentParent, "contentParent is not found");
            Assert.IsNotNull(scoreEntryUIPrefabObject, "scoreEntryUIPrefabObject is not found");
            Assert.IsNotNull(currentScore, "currentScore is not found");

            scoreListVariable.OnValueChanged -= UpdateList;
            scoreListVariable.OnValueChanged += UpdateList;
        }

        public void OnDisable()
        {
            scoreListVariable.OnValueChanged -= UpdateList;
        }

        private void UpdateList()
        {
            foreach (Transform entry in contentParent)
            {
                Destroy(entry.gameObject);
            }
            instantiedScoreEntrys.Clear();

            for (int i = 0; i < scoreListVariable.Count; i++)
            {
                IScoreEntry scoreEntry = Instantiate(scoreEntryUIPrefabObject, contentParent).GetComponent<IScoreEntry>();
                scoreEntry.Setup(scoreListVariable[i].Name, scoreListVariable[i].Score);
                instantiedScoreEntrys.Add(scoreEntry);
                if (scoreListVariable[i] == currentScore.Value)
                {
                    scoreEntry.HighlightScore();
                }
            }
        }

        public void OnValidate()
        {
            if (scoreEntryUIPrefabObject != null)
            {
                IScoreEntry scoreEntry = scoreEntryUIPrefabObject.GetComponent<IScoreEntry>();
                if (scoreEntry == null) { scoreEntryUIPrefabObject = null; }
                Assert.IsNotNull(scoreEntryUIPrefabObject, "scoreEntryPrefabObject does not contain IScoreEntry");
            }
        }
    }
}