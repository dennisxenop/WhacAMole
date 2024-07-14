using Dennis.Events;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Assertions;

public class HoleViewBehaviour : MonoBehaviour, IHoleView, IClick
{
    [SerializeField]
    private GameEvent clickEvent;

    private bool isActive;

    public bool IsActive => isActive;

    public void OnClick()
    {
        Assert.IsNotNull(clickEvent, "clickEvent is not found");
        clickEvent.Raise();
        gameObject.SetActive(false);
        isActive = false;
    }

    public void SetActiveState(bool isActive)
    {
        this.isActive = isActive;
        gameObject.SetActive(isActive);
    }
}
