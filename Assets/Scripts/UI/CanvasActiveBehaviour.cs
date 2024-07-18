using Dennis;
using Dennis.Common;
using UnityEngine;
using UnityEngine.Assertions;

public class CanvasActiveBehaviour : MonoBehaviour, IActivatable
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField, Header("Is Active")]
    private float alphaActive = 1;

    [SerializeField]
    private bool interactableActive = true;

    [SerializeField]
    private bool blocksRaycastsActive = true;

    [SerializeField]
    private bool ignoreParentGroupsActive = false;

    [SerializeField, Header("Is Not Active")]
    private float alphaNotActive = 0;

    [SerializeField]
    private bool interactableNotActive = false;

    [SerializeField]
    private bool blocksRaycastsNotActive = false;

    [SerializeField]
    private bool ignoreParentGroupsNotActive = false;

    public void OnEnable()
    {
        Assert.IsNotNull(canvasGroup, "canvasGroup cannot be null.");
    }

    public void SetActive(bool isActive)
    {
        canvasGroup.alpha = isActive ? alphaActive : alphaNotActive;
        canvasGroup.interactable = isActive ? interactableActive : interactableNotActive;
        canvasGroup.blocksRaycasts = isActive ? blocksRaycastsActive : blocksRaycastsNotActive;
        canvasGroup.ignoreParentGroups = isActive ? ignoreParentGroupsActive : ignoreParentGroupsNotActive;
    }
}