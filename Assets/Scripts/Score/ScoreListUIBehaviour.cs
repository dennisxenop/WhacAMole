using Dennis.Score;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ScoreListUIBehaviour : MonoBehaviour
{
    [SerializeField]
    private ScoreListVariable scoreListVariable;

    [SerializeField]
    private Transform contentParent;

    [SerializeField]
    private GameObject scoreEntryPrefabObject;

    private List<IScoreEntry> instantiedScoreEntrys = new List<IScoreEntry>();
    public void OnEnable()
    {
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
            IScoreEntry scoreEntry = Instantiate(scoreEntryPrefabObject, contentParent).GetComponent<IScoreEntry>();
            scoreEntry.Setup(scoreListVariable[i].Name, scoreListVariable[i].Score);
            instantiedScoreEntrys.Add(scoreEntry);
        }
    }

    public void OnValidate()
    {
        if (scoreEntryPrefabObject != null)
        {
            IScoreEntry scoreEntry = scoreEntryPrefabObject.GetComponent<IScoreEntry>();
            if (scoreEntry == null) { scoreEntryPrefabObject = null; }
            Assert.IsNotNull(scoreEntryPrefabObject, "scoreEntryPrefabObject does not contain IScoreEntry");
        }
    }
}
