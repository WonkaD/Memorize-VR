using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicMenuController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitGame()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void ChangeGameMode()
    {
        _playerController.ChangeGameMode();
    }
}
