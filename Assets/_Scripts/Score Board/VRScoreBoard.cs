using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class VRScoreBoard : MonoBehaviour {

    [SerializeField]
    private VRInteractiveItem _interactiveItem;
    [SerializeField]
    private AudioClip _overAudio;
    [SerializeField]
    private AudioClip _selectAudio;
    [SerializeField]
    private Animator animator;

    public ScoreBoard ScoreBoard;
    public Light Light;
    private void OnEnable()
    {
        _interactiveItem.OnOver += HandleOver;
        _interactiveItem.OnOut += HandleOut;
        _interactiveItem.OnClick += HandleClick;
        _interactiveItem.OnDoubleClick += HandleDoubleClick;
    }


    private void OnDisable()
    {
        _interactiveItem.OnOver -= HandleOver;
        _interactiveItem.OnOut -= HandleOut;
        _interactiveItem.OnClick -= HandleClick;
        _interactiveItem.OnDoubleClick -= HandleDoubleClick;
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
    private void HandleClick()
    {
        //TODO teletransportar y enegrecer la pantalla
        animator.SetTrigger("Action");
        Light.enabled = !Light.enabled;
        gameObject.GetComponent<AudioSource>().PlayOneShot(_selectAudio);
    }

    //Handle the DoubleClick event
    private void HandleDoubleClick()
    {
        //TODO por ahora nada
    }
}
