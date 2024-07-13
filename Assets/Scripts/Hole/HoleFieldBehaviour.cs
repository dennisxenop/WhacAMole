using Dennis.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class HoleFieldBehaviour : MonoBehaviour
{
    [SerializeField]
    private HolesListVariable holesListVariable;

    private List<HoleBehaviour> holeListQueue = new List<HoleBehaviour>();

    [SerializeField]
    private BoolVariable roundRunning;



    public void OnEnable()
    {
        roundRunning.OnValueChanged -= roundRunningChanged;
        roundRunning.OnValueChanged += roundRunningChanged;

    }

    public void Start()
    {
        EnqueueHoleBehaviours();
    }

    private void EnqueueHoleBehaviours()
    {
        foreach (HoleBehaviour hole in holesListVariable.Value)
        {
            holeListQueue.Add(hole);
        }

        ListUtility.Shuffle(holeListQueue);
    }

    private void roundRunningChanged(bool isRunning)
    {
        if (!isRunning) return;
        StartCoroutine(routine());
    }

    private IEnumerator routine()
    {
        while (roundRunning.Value)
        {
            HoleBehaviour holeBehaviour = holesListVariable.Value[UnityEngine.Random.Range(0, holesListVariable.Value.Count)];
            Assert.IsNotNull(holeBehaviour, "holeBehaviour is not found");
            holeBehaviour.PopHole(UnityEngine.Random.value > 0.5f, UnityEngine.Random.Range(1, 4));
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 2));
        }

    }
}
