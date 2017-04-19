﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class VRTeleportItem : MonoBehaviour {

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
        PlayAudioOver();
    }

    private void PlayAudioOver()
    {
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
        PlayAudioClick();
        TeleportPlayer();
        Fade();
    }

    private void Fade()
    {
        var vrCameraFade = MainCamera.GetComponent<VRCameraFade>();
        if (vrCameraFade == null)
            Debug.Log("VrCameraFade is NULL");
        else
            vrCameraFade.FadeIn(1, false);
    }

    private void TeleportPlayer()
    {
        Player.transform.position = gameObject.transform.position + new Vector3(0, 2.3f, 0);
        Player.transform.rotation = gameObject.transform.rotation;
        Player.transform.localRotation = gameObject.transform.localRotation;
        MainCamera.transform.rotation = gameObject.transform.rotation;
    }

    private void PlayAudioClick()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_selectAudio);
    }


    //Handle the DoubleClick event
    private void HandleDoubleClick()
    {
        //TODO por ahora nada
    }
}
