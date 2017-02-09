using UnityEngine;

namespace Assets.Scripts
{
    public class RoomGame1 : RoomGame {

        [SerializeField] private OfficeGameController gameController;

        // Use this for initialization
        void Start () {
		    
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        public override void StartGame()
        {
            gameController.StartLevel(1);
            Debug.Log("Empezando juego...");
        }

        public override void FinishGame()
        {
            gameController.FinishLevel(1, new Punctuation());
        }
    }
}
