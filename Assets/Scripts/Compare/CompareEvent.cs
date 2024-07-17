using UnityEngine;

namespace Dennis
{
    [System.Serializable]
    public class CompareEvent<T, TV>
    {
        [SerializeField]
        private T variable;
        public T Variable { get { return variable; } }
        [SerializeField]
        private TV compareVariable;
        public TV CompareVariable { get { return compareVariable; } }

    }

}