using Core;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("HUD")] [SerializeField] private TextMeshProUGUI scoreText;

        [SerializeField] private TextMeshProUGUI livesText;

        [Header("Game Over UI")] [SerializeField]
        private GameObject gameOverPanel;

        [SerializeField] private TextMeshProUGUI scoreValueText;
        [SerializeField] private TextMeshProUGUI bestScoreValueText;

        protected override void Awake()
        {
            base.Awake();

            gameOverPanel.SetActive(false);
        }

        public void DisplayScore(int score)
        {
            scoreText.text = score.ToString();
        }

        public void DisplayLives(int lives)
        {
            livesText.text = $"Lives: {lives}";
        }

        public void DisplayGameOver(int currentScore, int bestScore)
        {
            gameOverPanel.SetActive(true);
            scoreValueText.text = $"Score: {currentScore}";
            bestScoreValueText.text = $"Best: {bestScore}";
        }
    }
}