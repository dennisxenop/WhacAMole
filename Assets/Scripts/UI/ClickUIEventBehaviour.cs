using Dennis.Events;
using UnityEngine;

public class ClickUIEventBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameEvent clickEvent;

    public void OnClick()
    {
        clickEvent.Raise();
    }
}
