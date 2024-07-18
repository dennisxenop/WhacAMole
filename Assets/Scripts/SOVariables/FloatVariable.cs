using Dennis.Variable;
using UnityEngine;

namespace Dennis.Variables
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/FloatVariable")]
    public class FloatVariable : ScriptableObjectVariable<float>, ISOAccesableVariable<float>
    {
        public float Value
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