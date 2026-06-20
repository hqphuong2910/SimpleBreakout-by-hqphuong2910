using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private const int MaxScore = 999999999;
        private const int MaxLives = 3;
        private int _currentLives;
        private int _currentScore;
        public bool IsGameOver { get; private set; }

        protected override void Start()
        {
            base.Start();

            StartGame();
        }

        private void Update()
        {
            TriggerGameOver();
        }

        private void StartGame()
        {
            Time.timeScale = 1f;
            UpdateLives(MaxLives);
            UpdateScore(0);
        }

        /// <summary>
        ///     Increase/Decrease current score.
        /// </summary>
        /// <param name="score">Positive/Negative integer will increase/decrease current score.</param>
        public void UpdateScore(int score)
        {
            _currentScore += score;
            if (_currentScore > MaxScore) _currentScore = MaxScore;
            UIManager.Instance.DisplayScore(_currentScore);
        }

        /// <summary>
        ///     Increase/Decrease current lives.
        /// </summary>
        /// <param name="lives">Positive/Negative integer will increase/decrease current lives.</param>
        public void UpdateLives(int lives)
        {
            _currentLives += lives;
            _currentLives = _currentLives switch
            {
                < 0 => 0,
                > MaxLives => MaxLives,
                _ => _currentLives
            };
            UIManager.Instance.DisplayLives(_currentLives);
        }

        private void TriggerGameOver()
        {
            IsGameOver = _currentLives <= 0;
            if (!IsGameOver) return;
            Time.timeScale = 0f;
            var bestScore = PlayerPrefs.GetInt("BestScore", 0);
            if (_currentScore > bestScore)
            {
                bestScore = _currentScore;
                PlayerPrefs.SetInt("BestScore", bestScore);
            }

            UIManager.Instance.DisplayGameOver(_currentScore, bestScore);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}