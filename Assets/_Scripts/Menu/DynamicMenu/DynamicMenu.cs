using System.Linq;
using UnityEngine;

public class DynamicMenu : MonoBehaviour
{
    [SerializeField] private float _defaultDistance;
    [SerializeField] private Transform _cameraPlayer; // Reference to the script that fades the scene to black.
    [SerializeField] private bool useNormal;
    // Use this for initialization
    private Vector3 _originalScale;
    private void Start()
    {
        gameObject.SetActive(false);
        _originalScale = transform.localScale;
        _cameraPlayer = GameObject.FindGameObjectsWithTag("MainCamera").First(x=>x.name.Equals("CenterEyeAnchor")).transform;
    }

    // Update is called once per frame

    private void Update()
    {
    }

    public void ActiveMenu()
    {
        if (!IsVisible())
        {
            RaycastHit hit;
            if (RaycastHit(out hit))
                SetPosition(hit);
            else
                SetPosition();
        }
        gameObject.SetActive(!IsVisible());
    }

    private bool IsVisible()
    {
        return gameObject.activeSelf;
    }

    private bool RaycastHit(out RaycastHit hit)
    {
        return Physics.Raycast(_cameraPlayer.position, _cameraPlayer.forward, out hit, _defaultDistance);
    }

    private void SetPosition()
    {
        transform.position = _cameraPlayer.position + _cameraPlayer.forward * _defaultDistance;
        transform.LookAt(_cameraPlayer.position);
        transform.localScale = _originalScale * _defaultDistance;

    }

    private void SetPosition(RaycastHit hit)
    {
        transform.position = hit.point;
        transform.localScale = _originalScale * hit.distance;
        if (useNormal)
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
        else
            transform.LookAt(_cameraPlayer.position);
    }
}
