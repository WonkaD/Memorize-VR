using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class RaycasterMove : MonoBehaviour {
    [SerializeField] private Transform objectTransform;
    [SerializeField] private RectTransform pointerTransform;
    [SerializeField] private VREyeRaycaster raycaster;
    // Use this for initialization
    private Collision jointCollision;

    void Start ()
    {
        
    }

    void OnEnable()
    {
        raycaster.ChangeRayCastModeTo(RayMode.PickUp);
    }

    void OnDisable()
    {
        raycaster.ChangeRayCastModeTo(RayMode.Raycast);
        Drop();
    }
    // Update is called once per frame
    void Update () {

        var newPosition = new Vector3(pointerTransform.position.x, pointerTransform.position.y, pointerTransform.position.z);
        objectTransform.position = newPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("MobileObject") && !IsDragging())
            Drag(collision);
        else if (!collision.gameObject.tag.Equals("MobileObject") && IsDragging())
            Drop();

    }

    private bool IsDragging()
    {
        return jointCollision != null;
    }

    private void Drag(Collision collision)
    {
        jointCollision = collision;
        DesactivateGravity();
        CreateJoint();
    }

    private void Drop()
    {
        ActivateGravity();
        DestroyJoint();
        jointCollision = null;
    }

    private void DestroyJoint()
    {
        if (jointCollision == null) return;
        var fixedJoint = jointCollision.gameObject.GetComponent<FixedJoint>();
        if (fixedJoint == null) return;
        Destroy(fixedJoint);
    }

    private void CreateJoint()
    {

        FixedJoint joint = jointCollision.gameObject.AddComponent<FixedJoint>();
        var rb = GetRigidbody();
        jointCollision.gameObject.transform.position = gameObject.transform.position;
        if (rb == null) return;

        joint.connectedBody = rb;
    }

    private void ActivateGravity()
    {
        var rb = GetRigidbody();
        if (rb == null || jointCollision == null) return;
        jointCollision.rigidbody.useGravity = true;
        rb.useGravity = true;
        ResetPhysics(rb);
    }

    private void DesactivateGravity()
    {
        var rb = GetRigidbody();
        if (rb == null || jointCollision == null) return;
        jointCollision.rigidbody.useGravity = false;
        rb.useGravity = false;
        ResetPhysics(rb);
    }

    private void ResetPhysics(Rigidbody rb)
    {
        jointCollision.rigidbody.velocity = Vector3.zero;
        jointCollision.rigidbody.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private Rigidbody GetRigidbody()
    {
        return gameObject.GetComponent<Rigidbody>();
    }
}
