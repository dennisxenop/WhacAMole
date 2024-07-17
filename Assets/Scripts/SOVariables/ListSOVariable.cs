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
            value.Add(item);
            Invoke();
        }

        public void AddOnce(T item)
        {
            if(value.Contains(item)) { Debug.LogWarning("item already added"); return; }

            Add(item);
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

        public override void ResetSOValues()
        {
            if(resetValue == null) {
                value = new List<T>();
                resetValue = new List<T>();
            } else {
                value = new List<T>(resetValue);
            }
        }

#if UNITY_EDITOR
        public override void SetResetValue()
        {
            if(value == null) {
                value = new List<T>();
                resetValue = new List<T>();
            } else {
                resetValue = new List<T>(value);
            }
        }
#endif

    }
}