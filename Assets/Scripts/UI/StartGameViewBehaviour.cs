using Dennis.Events;
using Dennis.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameViewBehaviour : MonoBehaviour, IClick
{
    [SerializeField, RequireInterface(typeof(IGameEvent))]
    private Object startGameEventObject;
    private IGameEvent StartGameEvent => startGameEventObject as IGameEvent;

    public void OnClick()
    {
        StartGameEvent.Raise();
    }
}
