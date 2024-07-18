using UnityEngine;

namespace Dennis.Compare
{
    [System.Serializable]
    public class CompareEvent<T, TT>
    {
        [SerializeField]
        private T variable;

        public T Variable { get { return variable; } }

        [SerializeField]
        private TT compareVariable;

        public TT CompareVariable { get { return compareVariable; } }
    }
}