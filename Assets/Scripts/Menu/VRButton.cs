using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRStandardAssets.Utils;

[Serializable]
public class VRButtonEvent:UnityEvent<VRButton>{}


public class VRButton : MonoBehaviour
{ 
    [SerializeField] private MenuController menu;
    public Color NormalColor;
    public Color OverColor;
    public Color ClickedColor;
    [SerializeField] private VRButtonEvent _onClick;
    [SerializeField] private VRInteractiveItem interactiveItem;
    [SerializeField] private Image button;

    private void Awake()
    {
        button.color = NormalColor;
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        var buttonSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        boxCollider.size = new Vector3(buttonSize.x, buttonSize.y, 1);
    }


    private void OnEnable()
    {
        interactiveItem.OnOver += HandleOver;
        interactiveItem.OnOut += HandleOut;
        interactiveItem.OnClick += HandleClick;
    }


    private void OnDisable()
    {
        interactiveItem.OnOver -= HandleOver;
        interactiveItem.OnOut -= HandleOut;
        interactiveItem.OnClick -= HandleClick;
    }


    //Handle the Over event
    private void HandleOver()
    {
        button.color = OverColor;
    }


    //Handle the Out event
    private void HandleOut()
    {
        button.color = NormalColor;
    }


    //Handle the Click event
    private void HandleClick()
    {
        button.color = ClickedColor;
        if (_onClick != null)
        { // Trigger our callbacks
            _onClick.Invoke(this);
        }
    }
}
