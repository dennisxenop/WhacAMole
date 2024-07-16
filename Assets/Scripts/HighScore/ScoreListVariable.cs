using UnityEngine;
using Dennis.Variables;
using Dennis.HighScore;

[CreateAssetMenu(fileName = "ScoreListVariable", menuName = "Variables/ScoreListVariable")]
public class ScoreListVariable : ListSOVariable<HighScoreEntry>
{
}
