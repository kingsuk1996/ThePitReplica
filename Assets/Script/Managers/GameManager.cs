using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RedApple.ThePit
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager uIManager;
        [SerializeField] private PlayerController playerController;

        private GameState currentGameState;
        private int count;

        internal bool Once;
        public Toggle toggle;
        public static GameManager Instance;

        public GameState CurrentGameState { get => currentGameState; set => currentGameState = value; }


        private void Awake()
        {
            Instance = this;
            currentGameState = GameState.NotStart;
            uIManager.Init();
            playerController.Init();
        }

        internal void ChangeGameState(GameState _gameState)
        {
            currentGameState = _gameState;
            switch (currentGameState)
            {
                case GameState.NotStart:
                    break;

                case GameState.Instruction:
                    Once = true;
                    break;

                case GameState.Start:
                    StartCoroutine(Score());
                    break;

                case GameState.Finish:
                    uIManager.InitGameOver();
                    break;
            }
        }

        private IEnumerator Score()
        {
            while (CurrentGameState != GameState.Finish)
            {
                count++;
                uIManager.SetScore(count);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}