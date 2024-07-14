using System;
using UnityEngine;
using Dennis.Reset;

namespace Dennis.Variables
{
    public abstract class ScriptableObjectVariable<T> : ScriptableObject, IResetOnPlaymodeExit where T : new()
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public event Action OnValueChanged;

        protected T resetValue;

        [SerializeField]
        protected T value;

        private void OnValidate()
        {
            if (!Application.isPlaying) SetResetValue();
            Invoke();
        }

        protected void Invoke()
        {
            OnValueChanged?.Invoke();
        }

        public void SetResetValue()
        {
            resetValue = value == null ? value = new T() : value;
        }

        public void PlaymodeExitReset()
        {
            value = resetValue == null ? resetValue = new T() : resetValue;
        }
    }
}
