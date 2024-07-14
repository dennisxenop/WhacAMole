using Dennis.Variables;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class HoleBehaviour : MonoBehaviour, IHole
{
    [SerializeField, RequireInterface(typeof(IHoleView))]
    private Object moleViewObject;
    private IHoleView moleView => moleViewObject as IHoleView;

    [SerializeField, RequireInterface(typeof(IHoleView))]
    private Object nonMoleViewObject;
    private IHoleView nonMoleView => nonMoleViewObject as IHoleView;
    [SerializeField]
    private HolesListVariable holesList;

    private Coroutine popDurationCoroutine;

    private void OnEnable()
    {
        Assert.IsNotNull(holesList, "holesList is not assigned.");
        Assert.IsNotNull(moleView, "moleView is not assigned.");
        Assert.IsNotNull(nonMoleView, "nonMoleView is not assigned.");

        holesList.Add(this);
    }

    public void PopHole(bool isMole, float durationToPop)
    {
        if (popDurationCoroutine != null)
        {
            StopCoroutine(popDurationCoroutine);
        }

        holesList.Remove(this);
        popDurationCoroutine = StartCoroutine(PopDurationCoroutine(isMole, durationToPop));
    }

    private IEnumerator PopDurationCoroutine(bool isMole, float durationToPop)
    {
        SetActiveState(isMole, true);
        yield return new WaitForSeconds(durationToPop);
        SetActiveState(isMole, false);
        holesList.Add(this);

    }

    private void SetActiveState(bool isMole, bool isActive)
    {
        if (isMole)
        {
            moleView.SetActiveState(isActive);
        }
        else
        {
            nonMoleView.SetActiveState(isActive);
        }
    }
}