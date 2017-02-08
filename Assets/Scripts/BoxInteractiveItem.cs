using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;


public class BoxInteractiveItem : MonoBehaviour {

    [SerializeField]
    private Material m_NormalMaterial;
    [SerializeField]
    private Material m_OverMaterial;
    public Material m_ClickedMaterial;
    [SerializeField]
    private VRInteractiveItem interactiveItem;
    [SerializeField]
    private Renderer objectRenderer;

    private bool isSelected;
    public int hiddenValue;

    public bool IsSelected
    {
        get
        {
            return isSelected;
        }

        set
        {
            isSelected = value;
            if (!value) objectRenderer.material = m_NormalMaterial;
        }
    }

    private void Awake()
    {
        objectRenderer.material = m_NormalMaterial;
    }


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
        if (isSelected) return;
        objectRenderer.material = m_OverMaterial;
    }


    //Handle the Out event
    private void HandleOut()
    {
        if (isSelected) return;
        objectRenderer.material = m_NormalMaterial;
    }


    //Handle the Click event
    private void HandleClick()
    {
        isSelected = true;
        objectRenderer.material = m_ClickedMaterial;
    }


    //Handle the DoubleClick event
    private void HandleDoubleClick()
    {
        isSelected = false;
        objectRenderer.material = m_OverMaterial;
    }
}
