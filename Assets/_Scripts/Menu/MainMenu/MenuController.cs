using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField] private Image reticleImageLoad;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas menuConfirmationCanvas;

    public string PathFile = "saveGame.bin";
    void Awake()
    {

    }

    IEnumerator BackGroundLoadOffice(String scene)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        float progress = 0.0f;
        while (!async.isDone)
        {
            Debug.Log(async.progress);
            reticleImageLoad.fillAmount = (async.progress > progress) ? async.progress: progress;
            progress += 0.01f;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        

    }
    public void StartGame()
    {
        Debug.Log("Starting game...");
        StartCoroutine(BackGroundLoadOffice("Office"));
    }

    public void NewGame()
    {
        ShowConfirmationMenu();

    }

    private void ShowConfirmationMenu()
    {

        menuCanvas.enabled =false;
        menuConfirmationCanvas.enabled =true;
    }

    public void RemoveAndStartGame()
    {
        BinaryFileManager.Remove(PathFile);
        StartGame();
    }

    public void ShowMenuGame()
    {
        menuCanvas.enabled = true;
        menuConfirmationCanvas.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
