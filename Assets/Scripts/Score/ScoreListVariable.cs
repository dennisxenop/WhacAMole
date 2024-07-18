using Dennis.Variables;
using UnityEngine;

namespace Dennis.Score
{
    [CreateAssetMenu(fileName = "ScoreListVariable", menuName = "Variables/ScoreListVariable")]
    public class ScoreListVariable : ListSOVariable<ScoreEntry>
    {
    }
}