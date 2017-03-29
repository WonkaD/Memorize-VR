using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class RaycasterMove : MonoBehaviour {
    [SerializeField]
    private Transform objectTransform;

    [SerializeField]
    private RectTransform pointerTransform;

    [SerializeField] private VREyeRaycaster raycaster;


    // Use this for initialization
    void Start () {
		
	}

    void OnEnable()
    {
        raycaster.ChangeRayCastModeTo(RayMode.PickUp);
    }

    void OnDisable()
    {
        raycaster.ChangeRayCastModeTo(RayMode.Raycast);
    }
    // Update is called once per frame
    void Update () {
        var newPosition = new Vector3(pointerTransform.position.x, pointerTransform.position.y, pointerTransform.position.z);
        objectTransform.position = newPosition;
    }

    
}
