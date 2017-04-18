using UnityEngine;

public class DynamicMenu : MonoBehaviour
{
    [SerializeField] private float _defaultDistance;
    [SerializeField] private Transform _cameraPlayer; // Reference to the script that fades the scene to black.
    [SerializeField] private bool useNormal;
    // Use this for initialization

    private void Start()
    {
        gameObject.SetActive(false);
        _cameraPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame

    private void Update()
    {
    }

    public void ActiveMenu()
    {
        if (!gameObject.activeSelf)
        {
            RaycastHit hit;
            if (RaycastHit(out hit))
                SetPosition(hit);
            else
                SetPosition();
        }
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private bool RaycastHit(out RaycastHit hit)
    {
        return Physics.Raycast(_cameraPlayer.position, _cameraPlayer.forward, out hit, _defaultDistance);
    }

    private void SetPosition()
    {
        transform.position = _cameraPlayer.position + _cameraPlayer.forward * _defaultDistance;
        transform.LookAt(_cameraPlayer.position);
    }

    private void SetPosition(RaycastHit hit)
    {
        transform.position = hit.point;
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
    }
}
