using Dennis.Variables;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class HoleBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject moleGO;
    [SerializeField]
    private GameObject nonMoleGo;

    [SerializeField]
    private HolesListVariable holesList;

    private Coroutine popDurationCoroutine;

    private void OnEnable()
    {
        Assert.IsNotNull(holesList, "holesList is not assigned.");
        Assert.IsNotNull(moleGO, "moleGO is not assigned.");
        Assert.IsNotNull(nonMoleGo, "nonMoleGo is not assigned.");

        holesList.AddHole(this);
    }

    public void PopHole(bool isMole, float durationToPop)
    {
        if (popDurationCoroutine != null)
        {
            StopCoroutine(popDurationCoroutine);
        }
        popDurationCoroutine = StartCoroutine(PopDurationCoroutine(isMole, durationToPop));
    }

    private IEnumerator PopDurationCoroutine(bool isMole, float durationToPop)
    {
        SetActiveState(isMole, true);
        yield return new WaitForSeconds(durationToPop);
        SetActiveState(isMole, false);
    }

    private void SetActiveState(bool isMole, bool isActive)
    {
        if (isMole)
        {
            moleGO.SetActive(isActive);
        }
        else
        {
            nonMoleGo.SetActive(isActive);
        }
    }
}