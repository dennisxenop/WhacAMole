using System;
using UnityEngine;

namespace Dennis.Variables
{
    public abstract class ScriptableObjectVariable<T> : ScriptableObject where T : IEquatable<T>
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public event Action<T> OnValueChanged;

        [SerializeField]
        private T value;
        public T Value
        {
            get => value;
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }

        private void OnValidate()
        {
            OnValueChanged?.Invoke(value);
        }
    }
}
