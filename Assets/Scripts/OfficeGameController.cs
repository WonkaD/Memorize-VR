using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class OfficeGameController : MonoBehaviour
{
    private Dictionary<int, Level> levels;
    [SerializeField] private GameStatusHistoryMode statusGame;

	// Use this for initialization
	void Start ()
	{
	    statusGame.LoadStatus();
	    levels = statusGame.Levels;
	}

    void Finish()
    {
        statusGame.SaveStatus();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
