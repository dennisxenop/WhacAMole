using System;
using UnityEngine;
using Dennis.Reset;

namespace Dennis.Variables
{
    public abstract class ScriptableObjectVariable<T> : ScriptableObject, IResetOnPlaymodeExit
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public event Action<T> OnValueChanged;

        protected T resetValue;

        [SerializeField]
        protected T value;
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
            if (!Application.isPlaying) SetResetValue();
            OnValueChanged?.Invoke(value);
        }

        public virtual void SetResetValue()
        {
            resetValue = value;
        }

        public virtual void PlaymodeExitReset()
        {
            value = resetValue;
        }
    }
}
