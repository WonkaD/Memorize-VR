using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class OfficeGameController : MonoBehaviour
{
    [SerializeField] private List<Level> GameLevels;
    private GameStatusHistoryMode saveGame;

	// Use this for initialization
	void Start ()
	{
	    saveGame.LoadStatus();
	    loadLevels();
	}

    private void loadLevels()
    {
        var savedLevels = saveGame.Levels;
        if (savedLevels == null || GameLevels == null) return;
        foreach (var savedLevel in savedLevels)
        {
            if (GameLevels.Count >= savedLevel.Key) continue;
            RestoreLevel(savedLevel.Value, GameLevels[savedLevel.Key-1] );
        }
    }

    private static void RestoreLevel(GameStatusHistoryMode.Status savedLevel, Level gameLevel)
    {
        gameLevel.recordPunctuations = savedLevel.Punctuations;
        gameLevel.levelDoor.SetUnlock(savedLevel.UnlockStatus);
    }

    public void Finish()
    {
        saveGame.SaveStatus();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
