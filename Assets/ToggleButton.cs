using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private VRButton _vrButton;
        [SerializeField] private Text _buttonText;

        [SerializeField] private State FirstState;
        [SerializeField] private State SecondState;

        private Player _player;
        [SerializeField] private bool _firstState = true;

        // Use this for initialization
        public void Toogle()
        {
            TransformButton();
            _firstState = !_firstState;
        }

        private void TransformButton()
        {
            if (_firstState )
            {
                _buttonText.text = FirstState.StateButtonText;
                _vrButton._onClick = FirstState.StateEvent;
            }
            else
            {
                _buttonText.text = SecondState.StateButtonText;
                _vrButton._onClick = SecondState.StateEvent;
            }
        }

        public void SetFirstState(bool enable)
        {
            _firstState = enable;
            Toogle();
        }

        [Serializable]
        public class State
        {
            public string StateButtonText;
            public UnityEvent StateEvent;
        }
    }
}
