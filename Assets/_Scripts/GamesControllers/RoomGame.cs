using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GamesControllers
{
    public abstract class RoomGame : MonoBehaviour
    {
        public abstract void StartGame(EnumLevels difficulty);
        public abstract void FinishGame();
        public abstract void AbortGame();

        public abstract IEnumerator ClickEvent(GameObjectController selectedGameObject);

        void Start()
        {

        }

        // Update is called once per frame

        void Update()
        {

        }
    }
}