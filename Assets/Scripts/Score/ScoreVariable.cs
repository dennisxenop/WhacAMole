using Dennis.Variable;
using Dennis.Variables;
using UnityEngine;

namespace Dennis.Score
{
    [CreateAssetMenu(fileName = "ScoreVariable", menuName = "Variables/ScoreVariable")]
    public class ScoreVariable : ScriptableObjectVariable<ScoreEntry>, ISOAccesableVariable<ScoreEntry>
    {
        public ScoreEntry Value
        {
            get { return value; }
            set
            {
                this.value = value;
                Invoke();
            }
        }
    }
}