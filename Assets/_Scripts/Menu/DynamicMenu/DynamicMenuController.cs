using System.Collections.Generic;
using System.Linq;
using Assets;
using Assets._Scripts.GamesControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicMenuController : MonoBehaviour
{
    [SerializeField] private GameObject controller;
    [SerializeField] private OfficeGameController gameController;

    private List<AudioSource> _audioSources;
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
        gameController.Save();
        SceneManager.LoadScene("Lobby");
    }

    public void MuteGame()
    {
        FindAudioSourcesIfNotInicialized();
        _audioSources.ForEach(x => x.mute = true);
    }

    public void UnmuteGame()
    {
        FindAudioSourcesIfNotInicialized();
        _audioSources.ForEach(x => x.mute = false);
    }

    public void RemoveTutorials ()
    {
        Destroy(GameObject.FindGameObjectWithTag("Tutorial"));
    }

    private void FindAudioSourcesIfNotInicialized()
    {
        if (_audioSources == null) _audioSources = FindObjectsOfType<AudioSource>().ToList();
    }
}
