using System;
using UnityEngine;
using Dennis.Reset;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Dennis.Variables
{
    public abstract class ScriptableObjectVariable<T> : ScriptableObject, IResetSOValues

    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        [SerializeField]
        private bool resetOnSceneLoad;
        public ScriptableObjectVariable()
        {
            SceneManager.sceneUnloaded -= ResetValues;
            SceneManager.sceneUnloaded += ResetValues;
        }

        private void ResetValues(Scene arg0)
        {
            if (this != null)
            {
                if (!resetOnSceneLoad) return;
                ResetSOValues();
            }
        }

        public event Action OnValueChanged;

        protected T resetValue;

        [SerializeField]
        protected T value;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!Application.isPlaying) SetResetValue();
        }
        public virtual void SetResetValue()
        {
            resetValue = DeepCopy(value);
        }
#endif


        protected void Invoke()
        {
            OnValueChanged?.Invoke();
        }

        public virtual void ResetSOValues()
        {
            value = DeepCopy(resetValue);
        }

        private void OnEnable()
        {
            if (!IsReferenceType(typeof(T))) return;
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                value = Activator.CreateInstance<T>();
            }
            if (EqualityComparer<T>.Default.Equals(resetValue, default(T)))
            {
                resetValue = Activator.CreateInstance<T>();
            }
        }

        private T DeepCopy(T original)
        {
            if (original is ICloneable cloneable)
            {
                return (T)cloneable.Clone();
            }
            if (original == null || original.GetType().IsValueType || original is string)
            {
                return original;
            }

            throw new InvalidOperationException($"DeepCopy is not supported for type {typeof(T)}");
        }
        public static bool IsReferenceType(Type type)
        {
            return (type.IsClass || type.IsInterface || type.IsArray) && type != typeof(string);
        }
    }
}
