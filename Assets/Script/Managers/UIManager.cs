using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace RedApple.ThePit
{
    public class UIManager : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] private List<GameObject> Panels;

        [Space(20)]
        [SerializeField] private TMP_Text ScoreText;
        [SerializeField] private TMP_Text finalScoreText;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private TMP_Text fpsCounter;
        [SerializeField] private int setFps = 240;

        [Space(10)]
        [SerializeField] private RectTransform ThePit;
        [SerializeField] private RectTransform HowToPlay;
        [SerializeField] private RectTransform StartButton;

        [Space(10)]
        [SerializeField] private Animator playerAnimation;

        private int HighScore = 0;
        private int Score = 0;
        private float deltaTime;

        internal void Init()
        {
            Application.targetFrameRate = setFps;
            QualitySettings.vSyncCount = 0;
            Panelhandler(GameConstants.SplashPanel);
            ThePit.DOAnchorPosY(600, 2f).SetEase(Ease.OutBack).OnComplete( ()=> StartButton.DOAnchorPosY(100, 1f));

            Score = 0;
            HighScore = 0;
            highScoreText.text = "Best Score :: " + PlayerPrefs.GetInt(GameConstants.HighScore, HighScore).ToString();
        }

        private void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsCounter.text = "FPS : " + Mathf.Ceil(fps).ToString();
        }

        private void Panelhandler(string PanelID)
        {
            foreach (GameObject panel in Panels)
            {
                if (panel.GetComponent<PanelIDKeeper>().panelID == PanelID)
                {
                    panel.SetActive(true);
                }
                else
                {
                    panel.SetActive(false);
                }
            }
        }

        public void OnStartButtonClick()
        {
            Panelhandler(GameConstants.InstructionPanel);
            HowToPlay.DOAnchorPosY(650, 2f);
            GameManager.Instance.ChangeGameState(GameState.Instruction);
        }

        public void OnPlayButtonClick()
        {
            Panelhandler(GameConstants.GamePlayPanel);
            StartCoroutine(GameStart());
        }

        private IEnumerator GameStart()
        {
            yield return new WaitForSeconds(.1f);
            GameManager.Instance.ChangeGameState(GameState.Start);
        }

        internal void SetScore(int _score)
        {
            Score = _score;
            ScoreText.text = Score.ToString();
        }

        internal void InitGameOver()
        {
            StartCoroutine(GameOver());
        }

        private IEnumerator GameOver()
        {
            yield return new WaitForSeconds(.2f);
            playerAnimation.enabled = false;
            Panelhandler(GameConstants.GameOverPanel);
            finalScoreText.text = "Score : " + Score.ToString();
            highScoreText.text = "Best Score :: " + PlayerPrefs.GetInt(GameConstants.HighScore);
            CheckHighScore();
        }

        private void CheckHighScore()
        {
            if (Score > PlayerPrefs.GetInt(GameConstants.HighScore))
            {
                PlayerPrefs.SetInt(GameConstants.HighScore, Score);
                highScoreText.text = "Best Score :: " + Score.ToString();
            }
        }

        public void OnRestartButtonClick()
        {
            SceneManager.LoadScene(0);
        }

        internal void GameplayInstructionState(InstructionState _currentInstructionState)
        {
            switch (_currentInstructionState)
            {
                case InstructionState.jump:
                    break;
            }
        }
    }
}