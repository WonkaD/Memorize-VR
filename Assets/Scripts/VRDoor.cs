using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Maze;
using VRStandardAssets.Utils;

public class VRDoor : MonoBehaviour {

    [SerializeField] private VRInteractiveItem interactiveItem;
    [SerializeField] private AudioClip _overAudio;
    [SerializeField] private AudioClip _selectAudio;
    [SerializeField] private Animator animator;
    private bool block = true;
    private bool openDoor = false;
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


    public void SetBlock(bool status)
    {
        block = status;
    }

    public bool GetBlock()
    {
        return block;
    }

    //Handle the Over event
    private void HandleOver()
    {
        //TODO reproducir sonido
        if (!block) gameObject.GetComponent<AudioSource>().PlayOneShot(_overAudio);
    }


    //Handle the Out event
    private void HandleOut()
    {
        //TODO pensar algo

    }


    //Handle the Click event
    private void HandleClick()
    {
        //TODO teletransportar y enegrecer la pantalla
        if (!block)
        {
            animator.SetTrigger("Action");
            gameObject.GetComponent<AudioSource>().PlayOneShot(_selectAudio);
            openDoor = !openDoor;
        }
    }


    //Handle the DoubleClick event
    private void HandleDoubleClick()
    {
        //TODO por ahora nada
    }
}
