using UnityEngine;

namespace Assets
{
    public class Rotation : MonoBehaviour
    {
        public float Velocity = 1;

        // Use this for initialization
        void Start () {
            transform.rotation = new Quaternion(0,0,0,0);
        }
	
        // Update is called once per frame
        void Update () {
            transform.Rotate(Vector3.forward * Time.deltaTime* Velocity *10);
        }
    }
}
