using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

namespace Assets.VR.VRSampleScenes.Scripts.Utils
{
    // This class simply allows the user to return to the main menu.
    public class OpenMenu : MonoBehaviour
    {
        [SerializeField] private VRInput m_VRInput;                     // Reference to the VRInput in order to know when Cancel is pressed.
        [SerializeField] private DynamicMenu menu;

        private void OnEnable ()
        {
            m_VRInput.OnCancel += HandleCancel;
        }


        private void OnDisable ()
        {
            m_VRInput.OnCancel -= HandleCancel;
        }


        private void HandleCancel ()
        {
            menu.ActiveMenu();
        }


    }
}