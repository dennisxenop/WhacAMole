using UnityEngine;

namespace Dennis.Variables
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/FloatVariable")]
    public class FloatVariable : ScriptableObjectVariable<float>, ISOAccesableVariable<float>
    {
        public float Value { get => value; set => this.value = value; }
    }
}
