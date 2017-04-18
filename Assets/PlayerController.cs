using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private bool VRMode;
        
        [SerializeField] private FirstPersonController _firstPersonController;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private OVRCameraRig _ovrCameraRig;


        // Use this for initialization
        void Start ()
        {
            ChangePlayerMode();
        }

        private void ChangePlayerMode()
        {
            if (VRMode) ActivateVRMode();
            else DisableVRMode();
        }

        public void ChangeGameMode()
        {
            VRMode = !VRMode;
            ChangePlayerMode();

        }

        private void ActivateVRMode()
        {
            _firstPersonController.enabled = false;
            _characterController.enabled = false;
            _ovrCameraRig.enabled = true;
        }

        private void DisableVRMode()
        {
            _firstPersonController.enabled = true;
            _characterController.enabled = true;
            _ovrCameraRig.enabled = false;
        }

        // Update is called once per frame
        void Update () {
		
        }
    }
}
