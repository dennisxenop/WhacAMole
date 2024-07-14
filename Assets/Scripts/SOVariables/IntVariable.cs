using UnityEngine;

namespace Dennis.Variables
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/IntVariable")]
    public class IntVariable : ScriptableObjectVariable<int>, ISOAccesableVariable<int>
    {
        public int Value {
            get { return value; }
            set {
                this.value = value;
                Invoke();
            }
        }
    }
}