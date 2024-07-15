using UnityEngine;

namespace Dennis.Variables
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "Variables/BoolVariable")]
    public class BoolVariable : ScriptableObjectVariable<bool>, ISOAccesableVariable<bool>
    {
        public bool Value { get { return value; } set { this.value = value; Invoke(); } }
    }
}