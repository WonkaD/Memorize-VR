﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRStandardAssets.Utils;

    public class VRButton : MonoBehaviour
    { 
        public Color NormalColor;
        public Color OverColor;
        public Color ClickedColor;
        [SerializeField] private UnityEvent _onClick;
        [SerializeField] private VRInteractiveItem _interactiveItem;
        [SerializeField] private Image _button;

        private void Awake()
        {
            _button.color = NormalColor;
            var buttonSize = gameObject.GetComponent<RectTransform>().sizeDelta;
            gameObject.AddComponent<BoxCollider>().size = new Vector3(buttonSize.x, buttonSize.y, 1);
        }


        private void OnEnable()
        {
            _interactiveItem.OnOver += HandleOver;
            _interactiveItem.OnOut += HandleOut;
            _interactiveItem.OnClick += HandleClick;
        }


        private void OnDisable()
        {
            _interactiveItem.OnOver -= HandleOver;
            _interactiveItem.OnOut -= HandleOut;
            _interactiveItem.OnClick -= HandleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
            _button.color = OverColor;
        }


        //Handle the Out event
        private void HandleOut()
        {
            _button.color = NormalColor;
        }


        //Handle the Click event
        private void HandleClick()
        {
            _button.color = ClickedColor;
            if (_onClick != null)
            { // Trigger our callbacks
                _onClick.Invoke();
            }
        }
    }
