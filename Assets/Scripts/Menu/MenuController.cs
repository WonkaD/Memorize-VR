using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static EnumWorld World;
    public static EnumLevels Level;
    public static readonly Dictionary<EnumWorld, string> worlds = new Dictionary<EnumWorld, string>
    {
        {EnumWorld.OpenWorld, "OpenWorld"}, {EnumWorld.Room, "Room"}, { EnumWorld.Office, "Office"}
    };

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        
    }
    // Use this for initialization
    void Start () {
        World = EnumWorld.Room;
        Level = EnumLevels.Medium;
    }

    public void SetWorld(int world)
    {
        World = Enum.IsDefined(typeof(EnumWorld), world) ? (EnumWorld)world : EnumWorld.Room;
    }

    public void SetLevel (int level)
    {
        Level = Enum.IsDefined(typeof(EnumLevels), level) ? (EnumLevels) level: EnumLevels.Medium;
    }

    public void StartGame()
    {
        Debug.Log("Start game Level: " + Level + " and World: " + World);
        SceneManager.LoadScene(getSceneOfWorld());
    }

    private static string getSceneOfWorld()
    {
        String scene = "Lobby";
        worlds.TryGetValue(World, out scene);
        return scene;
    }
}
