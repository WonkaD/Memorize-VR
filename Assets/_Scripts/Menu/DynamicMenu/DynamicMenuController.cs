using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DynamicMenuController : MonoBehaviour
{
    [SerializeField] private GameObject controller;
	// Use this for initialization
	void Start ()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        controller.GetComponent<ToggleButton>().SetFirstState(player._firstPersonController.enabled);
        //controller.GetComponent<ToggleButton>().SetFirstState(true);
        controller.GetComponent<VRButton>().SetEnable(player._isVRPresent);
        //controller.GetComponent<VRButton>().SetEnable(true);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ExitGame()
    {
        SceneManager.LoadScene("Lobby");
    }

}
