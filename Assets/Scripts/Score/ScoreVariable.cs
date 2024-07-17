
using Dennis.Score;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Dennis.Variables
{
    [CreateAssetMenu(fileName = "ScoreVariable", menuName = "Variables/ScoreVariable")]
    public class ScoreVariable : ScriptableObjectVariable<ScoreEntry>, ISOAccesableVariable<ScoreEntry>
    {
        public ScoreEntry Value {
            get { return value; }
            set {
                this.value = value;
                Invoke();
            }
        }
    }
}
