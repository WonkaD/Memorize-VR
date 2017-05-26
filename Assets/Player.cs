using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using UnityStandardAssets.Characters.FirstPerson;
using VRStandardAssets.Utils;

namespace Assets
{
    public class Player : MonoBehaviour
    {
        public bool _isVRPresent { get; protected set; }
        public bool _isKeyBoardOrControllerPresent { get; protected set; }

        public FirstPersonController _firstPersonController;
        public OVRCameraRig _ovrCameraRig;
        public VRCameraFade _vrCameraFade;
        public Image _reticleImageLoad;

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
            _ovrCameraRig.usePerEyeCameras = _isVRPresent;
            _firstPersonController.enabled = !_isVRPresent;
        }

        public void SetReticleLoadPercentage(float percentage)
        {
            _reticleImageLoad.fillAmount = percentage;
        }

        public void DisableController()
        {
            if (_isVRPresent) _firstPersonController.enabled = false;
        }

        public void EnableController()
        {
            _firstPersonController.enabled = true;
        }

        public void FadeCamera()
        {
            
            if (_vrCameraFade == null)
                Debug.Log("VrCameraFade is NULL");
            else
                _vrCameraFade.FadeIn(1, false);
        }

    }
}
