using System;
using Assets.Scripts;
using UnityEngine;
using VRStandardAssets.Utils;

namespace Assets
{
    public class GameStartTeleport : MonoBehaviour
    {
        [SerializeField] private RoomGame _game;

        [SerializeField] private VRInteractiveItem interactiveItem;
        [SerializeField] private AudioClip _overAudio;
        [SerializeField] private AudioClip _selectAudio;

        private GameObject Player;
        private GameObject MainCamera;


        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }


        private void OnEnable()
        {
            interactiveItem.OnOver += HandleOver;
            interactiveItem.OnOut += HandleOut;
            interactiveItem.OnClick += HandleClick;
            interactiveItem.OnDoubleClick += HandleDoubleClick;
        }


        private void OnDisable()
        {
            interactiveItem.OnOver -= HandleOver;
            interactiveItem.OnOut -= HandleOut;
            interactiveItem.OnClick -= HandleClick;
            interactiveItem.OnDoubleClick -= HandleDoubleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
            //TODO reproducir sonido
            gameObject.GetComponent<AudioSource>().PlayOneShot(_overAudio);
        }


        //Handle the Out event
        private void HandleOut()
        {
            //TODO pensar algo

        }


        //Handle the Click event
        public void HandleClick()
        {
            //TODO teletransportar y enegrecer la pantalla
            Teleport();
            FadeCamera();
            StartGame();
        }

        private void StartGame()
        {
            _game.StartGame();
        }

        private void FadeCamera()
        {
            var vrCameraFade = MainCamera.GetComponent<VRCameraFade>();
            if (vrCameraFade == null)
                Debug.Log("VrCameraFade is NULL");
            else
                vrCameraFade.FadeIn(1, false);
        }

        private void Teleport()
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(_selectAudio);
            Player.transform.position = gameObject.transform.position + new Vector3(0, 2.3f, 0);
            Player.transform.rotation = gameObject.transform.rotation;
        }


        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
            //TODO por ahora nada
        }

    }
}
