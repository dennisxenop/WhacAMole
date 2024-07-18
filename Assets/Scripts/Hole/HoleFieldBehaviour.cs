using Dennis.Variables;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Dennis.Hole
{
    public class HoleFieldBehaviour : MonoBehaviour
    {
        [SerializeField]
        private HolesListVariable holesListVariable;

        [SerializeField]
        private BoolVariable roundRunning;

        private Coroutine routine;

        public void OnEnable()
        {
            Assert.IsNotNull(roundRunning, "roundRunning is not assigned in the inspector.");
            roundRunning.OnValueChanged += roundRunningChanged;
        }

        public void OnDisable()
        {
            if(roundRunning != null) {
                roundRunning.OnValueChanged -= roundRunningChanged;
            }
        }

        public void Start()
        {
            Assert.IsNotNull(holesListVariable, "holesListVariable is not assigned in the inspector.");
            Assert.IsNotNull(roundRunning, "roundRunning is not assigned in the inspector.");
        }

        private void roundRunningChanged()
        {
            if(roundRunning.Value) {
                if(routine != null) {
                    StopCoroutine(routine);
                }
                routine = StartCoroutine(PopHoleCoroutine());
            }
        }

        private IEnumerator PopHoleCoroutine()
        {
            while(roundRunning.Value) {
                IHole holeBehaviour = holesListVariable[Random.Range(0, holesListVariable.Count)];
                Assert.IsNotNull(holeBehaviour, "holeBehaviour is not found");
                holeBehaviour.PopHole(Random.value > 0.5f, Random.Range(1, 4));
                yield return new WaitForSeconds(Random.Range(1, 2));
            }
        }
    }
}