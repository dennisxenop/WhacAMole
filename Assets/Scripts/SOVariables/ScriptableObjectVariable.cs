using System;
using UnityEngine;
using Dennis.Reset;
using System.Collections.Generic;

namespace Dennis.Variables
{
    public abstract class ScriptableObjectVariable<T> :  ScriptableObject, IResetOnPlaymodeExit
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
            resetValue = value;
        }

        public void PlaymodeExitReset()
        {
            value = resetValue;
        }

        private void OnEnable()
        {
            // Ensure that value is initialized.
            if(EqualityComparer<T>.Default.Equals(value, default(T))) {
                value = Activator.CreateInstance<T>();
            }
        }
    }
}
