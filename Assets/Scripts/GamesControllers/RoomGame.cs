using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GamesControllers
{
    public abstract class RoomGame : MonoBehaviour
    {
        public abstract void StartGame();
        public abstract void FinishGame();
        public abstract IEnumerator ClickEvent(GameObjectController gameObjectController);



        void Start()
        {

        }

        // Update is called once per frame

        void Update()
        {

        }
    }
}