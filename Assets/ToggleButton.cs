﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private VRButton _vrButton;
        [SerializeField] private Text _buttonText;

        [SerializeField] private string _enableString;
        [SerializeField] private UnityEvent _enableEvent;
        
        [SerializeField] private string _disableString;
        [SerializeField] private UnityEvent _disableEvent;

        private Player _player;
        [SerializeField] private bool _enabled = true;

        // Use this for initialization
        public void Toogle()
        {
            TransformButton();
            _enabled = !_enabled;
        }

        private void TransformButton()
        {
            if (_enabled)
            {
                _buttonText.text = _enableString;
                _vrButton._onClick = _enableEvent;
            }
            else
            {
                _buttonText.text = _disableString;
                _vrButton._onClick = _disableEvent;
            }
        }

        public void setEnable(bool enable)
        {
            _enabled = enable;
            TransformButton();
        }
    }
}