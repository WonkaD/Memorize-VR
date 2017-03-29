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
    [SerializeField] private ConfirmationDialog confirmDialog;
    [SerializeField] private Canvas menuGameObject;
    [SerializeField] private Canvas menuDuplicationGameObject;

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
        StartCoroutine(WaitForCloseMenu());
        
    }

    private IEnumerator WaitForCloseMenu()
    {
        ShowConfirmationMenu();
        yield return new WaitWhile(() =>confirmDialog.GetVisible());
        if (confirmDialog.GetConfirmation())
            RemoveAndStartGame();
        else
            ShowMenuGame();
    }

    private void ShowConfirmationMenu()
    {

        menuGameObject.enabled =false;
        menuDuplicationGameObject.enabled =false;
        confirmDialog.SetVisible(true);
    }

    public void RemoveAndStartGame()
    {
        BinaryFileManager.Remove(PathFile);
        StartGame();
    }

    public void ShowMenuGame()
    {
        menuGameObject.enabled = true;
        menuDuplicationGameObject.enabled = true;
        confirmDialog.SetVisible(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
