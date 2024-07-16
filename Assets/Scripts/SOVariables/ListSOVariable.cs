using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dennis.Variables
{
    public class ListSOVariable<T> : ScriptableObjectVariable<List<T>>, ISOAccesableListVariable<T>, IReadOnlyList<T>
    {
        public T this[int index] => value[index];

        public int Count => value.Count;

        public void Add(T item)
        {
            if(value.Contains(item)) { Debug.LogWarning("item already added"); return; }

            value.Add(item);
            Invoke();
        }

        public void Set(List<T> items)
        {
            value = new List<T>(items);
            Invoke();
        }

        public void Remove(T item)
        {
            value.Remove(item);
            Invoke();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return value.GetEnumerator();
        }
    }
}