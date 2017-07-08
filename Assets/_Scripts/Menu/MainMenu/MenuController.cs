using System.Collections;
using Assets;
using Assets.Scripts;
using Assets._Scripts.Save_Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas menuDuplicateCanvas;
    [SerializeField] private Canvas menuConfirmationCanvas;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        SaveGameManager.Create();
    }

    private IEnumerator BackGroundLoadOffice(int scene)
    {
        var async = SceneManager.LoadSceneAsync(scene);
        var progress = 0.0f;
        while (!async.isDone)
        {
            Debug.Log(async.progress);
            _player.SetReticleLoadPercentage(async.progress > progress ? async.progress : progress);
            progress += 0.01f;
            yield return new WaitForSecondsRealtime(0.1f);
        }

        _player.SetReticleLoadPercentage(0);
    }

    public void StartGame()
    {
        Debug.Log("Starting game...");
        StartCoroutine(BackGroundLoadOffice(1));
    }

    public void NewGame()
    {
        ShowConfirmationMenu();
    }

    private void ShowConfirmationMenu()
    {
        menuCanvas.enabled = false;
        menuDuplicateCanvas.enabled = false;
        menuConfirmationCanvas.enabled = true;
    }

    public void RemoveAndStartGame()
    {
        SaveGameManager.Remove();
        StartGame();
    }

    public void ShowMenuGame()
    {
        menuCanvas.enabled = true;
        menuDuplicateCanvas.enabled = true;
        menuConfirmationCanvas.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
