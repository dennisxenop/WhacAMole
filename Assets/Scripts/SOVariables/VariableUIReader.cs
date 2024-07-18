using Dennis.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class VariableUIReader<T, TT> : MonoBehaviour where T : ScriptableObjectVariable<TT>, ISOAccesableVariable<TT>
{
    [SerializeField]
    protected T variableToRead;

    [SerializeField]
    protected TextMeshProUGUI textObject;

    private void OnEnable()
    {
        Assert.IsNotNull(variableToRead, "variableToRead is not assigned.");
        Assert.IsNotNull(textObject, "textObject is not assigned.");

        variableToRead.OnValueChanged -= CurrentVariableChanged;
        variableToRead.OnValueChanged += CurrentVariableChanged;

        CurrentVariableChanged();
    }

    public virtual void CurrentVariableChanged()
    {
        var textToRead = variableToRead.Value;
        if(textToRead != null) {
            textObject.text = textToRead.ToString();
        }
    }

    private void OnDisable()
    {
        variableToRead.OnValueChanged -= CurrentVariableChanged;
    }
}