using UnityEngine;

namespace Dennis.Variables
{
    [CreateAssetMenu(fileName = "StringVariable", menuName = "Variables/StringVariable")]
    public class StringVariable : ScriptableObjectVariable<string>, ISOAccesableVariable<string>
    {
        public string Value {
            get { return value; }
            set {
                this.value = value;
                Invoke();
            }
        }
    }
}