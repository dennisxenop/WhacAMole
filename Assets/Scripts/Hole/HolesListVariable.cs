using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dennis.Variables
{
    [CreateAssetMenu(fileName = "HoleListVariable", menuName = "Variables/HoleListVariable")]

    public class HolesListVariable : ScriptableObjectVariable<List<IHole>>, ISOAccesableListVariable<IHole>, IReadOnlyList<IHole>
    {
        public IHole this[int index] => value[index];

        public int Count => value.Count;

        public void Add(IHole holeBehaviour)
        {
            if (value.Contains(holeBehaviour)) { Debug.LogWarning("Holebehaviour already added"); return; }

            value.Add(holeBehaviour);
            Invoke();
        }

        public IEnumerator<IHole> GetEnumerator()
        {
            return value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return value.GetEnumerator();
        }
    }
}