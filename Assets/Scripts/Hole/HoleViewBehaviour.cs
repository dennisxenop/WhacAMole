using Dennis.Events;
using UnityEngine;
using UnityEngine.Assertions;

public class HoleViewBehaviour : MonoBehaviour, IHoleView, IClick
{
    [SerializeField]
    private GameEvent clickEvent;

    public void OnClick()
    {
        Assert.IsNotNull(clickEvent, "clickEvent is not found");
        clickEvent.Raise();
    }

    public void SetActiveState(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
