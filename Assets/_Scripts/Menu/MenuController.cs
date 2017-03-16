using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private static EnumWorld _world;
    public  static EnumLevels Level;
    [SerializeField] private Image reticleImageLoad;
    private  static readonly Dictionary<EnumWorld, string> worlds = new Dictionary<EnumWorld, string>
    {
        {EnumWorld.OpenWorld, "OpenWorld"}, {EnumWorld.Room, "Room"}, { EnumWorld.Office, "Office"}
    };
    

    void Awake()
    {
        DontDestroyOnLoad(this);
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

    // Use this for initialization
    void Start () {
        _world = EnumWorld.Office;
        Level = EnumLevels.Medium;
    }

    public void SetWorld(int world)
    {
        _world = Enum.IsDefined(typeof(EnumWorld), world) ? (EnumWorld)world : EnumWorld.Office;
    }

    public void SetLevel (int level)
    {
        Level = Enum.IsDefined(typeof(EnumLevels), level) ? (EnumLevels) level: EnumLevels.Medium;
    }

    public void StartGame()
    {
        Debug.Log("Starting game...");
        StartCoroutine(BackGroundLoadOffice(getSceneOfWorld()));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private static string getSceneOfWorld()
    {
        String scene = "Lobby";
        worlds.TryGetValue(_world, out scene);
        return scene;
    }
}
