using Dennis.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelViewBehaviour : MonoBehaviour
{
    [SerializeField]
    private BoolVariable boolVariable;

    [SerializeField]
    private bool invert;

    public void OnEnable()
    {
        boolVariable.OnValueChanged -= boolChanged;
        boolVariable.OnValueChanged += boolChanged;
    }

    public void OnDisable()
    {
        boolVariable.OnValueChanged -= boolChanged;
    }

    private void boolChanged()
    {
        bool test = invert ? !boolVariable.Value : boolVariable.Value;
        gameObject.SetActive(test);
    }
}
