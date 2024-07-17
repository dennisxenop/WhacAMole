using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using Dennis.Variables;

namespace Dennis
{
    public partial class VariableMultiCompareEvent<T, TV> : MonoBehaviour where T : ScriptableObjectVariable<TV>,ISOAccesableVariable<TV>
    {
        [SerializeField]
        private List<CompareEvent<T, TV>> any = new List<CompareEvent<T, TV>>();
        [SerializeField]
        private List<CompareEvent<T, TV>> all = new List<CompareEvent<T, TV>>();

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