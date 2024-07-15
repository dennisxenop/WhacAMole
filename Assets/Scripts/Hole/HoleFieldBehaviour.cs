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
        Assert.IsNotNull(roundRunning, "roundRunning is not assigned in the inspector.");
        roundRunning.OnValueChanged += roundRunningChanged;
    }

    public void OnDisable()
    {
        if (roundRunning != null)
        {
            roundRunning.OnValueChanged -= roundRunningChanged;
        }
    }

    public void Start()
    {
        Assert.IsNotNull(holesListVariable, "holesListVariable is not assigned in the inspector.");
        Assert.IsNotNull(roundRunning, "roundRunning is not assigned in the inspector.");

        EnqueueHoleBehaviours();
    }

    private void EnqueueHoleBehaviours()
    {
        foreach (HoleBehaviour hole in holesListVariable)
        {
            Assert.IsNotNull(hole, "hole in holesListVariable is null.");
            holeListQueue.Add(hole);
        }

        ListUtility.Shuffle(holeListQueue);
    }

    private void roundRunningChanged()
    {
        if (roundRunning.Value)
        {
            StartCoroutine(routine());
        }
    }

    private IEnumerator routine()
    {
        while (roundRunning.Value)
        {
            IHole holeBehaviour = holesListVariable[UnityEngine.Random.Range(0, holesListVariable.Count)];
            Assert.IsNotNull(holeBehaviour, "holeBehaviour is not found");
            holeBehaviour.PopHole(UnityEngine.Random.value > 0.5f, UnityEngine.Random.Range(1, 4));
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 2));
        }
    }
}