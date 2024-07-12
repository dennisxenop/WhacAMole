using System.Collections;
using System.Collections.Generic;
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

    public void OnEnable()
    {
        Assert.IsNotNull(holesList);
        Assert.IsNotNull(moleGO);
        Assert.IsNotNull(nonMoleGo);

        holesList.AddHole(this);
    }
}
