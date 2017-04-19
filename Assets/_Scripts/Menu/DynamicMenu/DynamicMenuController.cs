﻿using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicMenuController : MonoBehaviour
{
    [SerializeField] private GameObject controller;
	// Use this for initialization
	void Start ()
	{
	    var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	    controller.GetComponent<ToggleButton>().setEnable(player._firstPersonController.enabled);
	    if (!player._isVRPresent) controller.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitGame()
    {
        SceneManager.LoadScene("Lobby");
    }

}
