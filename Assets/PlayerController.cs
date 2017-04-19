using UnityEngine;
using UnityEngine.VR;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets
{
    public class PlayerController : MonoBehaviour
    {
        private bool _isVRPresent;
        private bool _isKeyBoardOrControllerPresent;

        [SerializeField] private FirstPersonController _firstPersonController;
        [SerializeField] private OVRPlayerController _ovrPlayerController;
        [SerializeField] private CharacterController _characterController;

        [SerializeField] private OVRCameraRig _ovrCameraRig;


        // Use this for initialization
        void Start ()
        {
            _isKeyBoardOrControllerPresent = Application.platform != RuntimePlatform.Android || Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0].Contains("Controller");
            _isVRPresent = VRDevice.isPresent;
            UseBestPlayerMode();
        }

        private void UseBestPlayerMode()
        {
            _ovrCameraRig.enabled = _isVRPresent;
            _firstPersonController.enabled = !_isVRPresent;
        }

        public void ChangeGameMode()
        {

        }



        private void ActivateVRMode()
        {
            if (VRDevice.isPresent)
            {
                _firstPersonController.enabled = false;
                _ovrCameraRig.enabled = true;
            }
            
        }

        private void DisableVRMode()
        {
            if (Input.mousePresent)
            {
                _firstPersonController.enabled = true;
                _ovrCameraRig.enabled = false;
            }
        }

        // Update is called once per frame
        void Update () {
		
        }
    }
}
