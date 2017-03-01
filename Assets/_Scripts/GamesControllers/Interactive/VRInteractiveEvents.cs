using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRStandardAssets.Utils;

public class VRInteractiveEvents : MonoBehaviour {

    
    public VRInteractiveItem interactiveItem;
    public UnityEvent OnClick;
    public UnityEvent OnOver;
    public UnityEvent OnOut;
    public UnityEvent OnDoubleClick;



    private void OnEnable()
    {
        interactiveItem.OnOver += HandleOver;
        interactiveItem.OnOut += HandleOut;
        interactiveItem.OnClick += HandleClick;
        interactiveItem.OnDoubleClick += HandleDoubleClick;
    }


    private void OnDisable()
    {
        interactiveItem.OnOver -= HandleOver;
        interactiveItem.OnOut -= HandleOut;
        interactiveItem.OnClick -= HandleClick;
        interactiveItem.OnDoubleClick -= HandleDoubleClick;
    }


    //Handle the Over event
    private void HandleOver()
    {
        if (OnOver != null) OnOver.Invoke();
    }


    //Handle the Out event
    private void HandleOut()
    {
        if (OnOut != null) OnOut.Invoke();
    }


    //Handle the Click event
    private void HandleClick()
    {
        if (OnClick != null) OnClick.Invoke();
    }


    //Handle the DoubleClick event
    private void HandleDoubleClick()
    {
        if (OnDoubleClick != null) OnDoubleClick.Invoke();
    }
}
