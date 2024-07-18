using Dennis.Variable;
using Dennis.Variables;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Dennis.Compare
{
    public abstract class VariableMultiCompareEvent<T, TT> : MonoBehaviour where T : ScriptableObjectVariable<TT>, ISOAccesableVariable<TT>
    {
        [SerializeField]
        private List<CompareEvent<T, TT>> any = new List<CompareEvent<T, TT>>();

        [SerializeField]
        private List<CompareEvent<T, TT>> all = new List<CompareEvent<T, TT>>();

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent<bool> Response;

        [Tooltip("Response to invoke when Event is raised and true.")]
        public UnityEvent<bool> ResponseTrue;

        [Tooltip("Response to invoke when Event is raised and false.")]
        public UnityEvent<bool> ResponseFalse;

        [SerializeField]
        private bool subscribeWhenDeactived;

        private bool subscribed;

        public VariableMultiCompareEvent()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMethod)
        {
            if(subscribeWhenDeactived && this != null && !subscribed) {
                Subscribe();
            }
        }

        private void Subscribe()
        {
            for(int i = 0; i < any.Count; i++) {
                any[i].Variable.OnValueChanged -= Changed;
                any[i].Variable.OnValueChanged += Changed;
            }
            for(int i = 0; i < all.Count; i++) {
                all[i].Variable.OnValueChanged -= Changed;
                all[i].Variable.OnValueChanged += Changed;
            }
            subscribed = true;
            Changed();
        }

        public void OnEnable()
        {
            if(!subscribeWhenDeactived && !subscribed) {
                Subscribe();
                Changed();
            }
        }

        public void OnDisable()
        {
            if(!subscribeWhenDeactived) {
                Unsubscribe();
            }
        }

        private void Unsubscribe()
        {
            for(int i = 0; i < any.Count; i++) {
                any[i].Variable.OnValueChanged -= Changed;
            }

            for(int i = 0; i < all.Count; i++) {
                all[i].Variable.OnValueChanged -= Changed;
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Destroy()
        {
            Unsubscribe();
        }

        protected virtual void Changed()
        {
            bool anyResponse = true;
            bool allResponse = true;
            if(any.Count > 0) {
                anyResponse = any.Any(x => x.CompareVariable.Equals(x.Variable.Value));
            }
            if(all.Count > 0) {
                allResponse = all.All(x => x.CompareVariable.Equals(x.Variable.Value));
            }

            bool response = anyResponse && allResponse;

            Response.Invoke(response);
            if(response) {
                ResponseTrue.Invoke(response);
            } else {
                ResponseFalse.Invoke(response);
            }
        }
    }
}