using Dennis.Variables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class StringVariableReaderUIBehaviour : MonoBehaviour
{
    [SerializeField]
    private StringVariable stringVariable;

    [SerializeField]
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        Assert.IsNotNull(stringVariable, "currentScore is not assigned.");
        Assert.IsNotNull(text, "scoreText is not assigned.");

        stringVariable.OnValueChanged -= StringVariableChanged;
        stringVariable.OnValueChanged += StringVariableChanged;

        StringVariableChanged();
    }

    private void StringVariableChanged()
    {
        text.text = stringVariable.Value;
    }

    private void OnDisable()
    {
        stringVariable.OnValueChanged -= StringVariableChanged;
    }
}
