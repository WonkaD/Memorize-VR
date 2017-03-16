using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMenu : MonoBehaviour
{
    [SerializeField] private float _defaultDistance;
    [SerializeField] private Transform _cameraPlayer;           // Reference to the script that fades the scene to black.
    [SerializeField] private bool useNormal;
    // Use this for initialization

    void Start () {
	    gameObject.SetActive(false);	
	}

    // Update is called once per frame

    void Update () {
		
	}

    public void ActiveMenu()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(_cameraPlayer.position, _cameraPlayer.forward, out hit, _defaultDistance))
            SetPosition(hit);
        else
            SetPosition();
        gameObject.SetActive(true);

    }

    private void SetPosition()
    {
        transform.position = _cameraPlayer.position + _cameraPlayer.forward * _defaultDistance;
        transform.LookAt((_cameraPlayer.position));
    }

    private void SetPosition(RaycastHit hit)
    {
        transform.position = hit.point;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
    }
}
