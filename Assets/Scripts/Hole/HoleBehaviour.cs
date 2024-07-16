using Dennis.Variables;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

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
    private float currentTime;
    private WaitForEndOfFrame waitForEndOfFrame;

    private void Awake()
    {
        waitForEndOfFrame = new WaitForEndOfFrame();
    }
    private void OnEnable()
    {
        Assert.IsNotNull(holesList, "holesList is not assigned.");
        Assert.IsNotNull(moleView, "moleView is not assigned.");
        Assert.IsNotNull(nonMoleView, "nonMoleView is not assigned.");
        holesList.AddOnce(this);

    }

    public void PopHole(bool isMole, float durationToPop)
    {
        if(popDurationCoroutine != null) {
            StopCoroutine(popDurationCoroutine);
        }

        currentTime = 0;
        holesList.Remove(this);
        popDurationCoroutine = StartCoroutine(PopDurationCoroutine(isMole, durationToPop));
    }

    private IEnumerator PopDurationCoroutine(bool isMole, float durationToPop)
    {
        SetActiveState(isMole, true);
        while(currentTime < durationToPop && GetHoleView(isMole).IsActive) {
            currentTime += Time.deltaTime;
            yield return waitForEndOfFrame;
        }
        SetActiveState(isMole, false);
        holesList.Add(this);

    }

    private IHoleView GetHoleView(bool isMole)
    {
        return isMole ? moleView : nonMoleView;
    }

    private void SetActiveState(bool isMole, bool isActive)
    {
        GetHoleView(isMole).SetActiveState(isActive);
    }
}