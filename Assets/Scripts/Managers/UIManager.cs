using Core;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("Gameplay Scene UI")] [SerializeField]
        private TextMeshProUGUI scoreText;

        [SerializeField] private TextMeshProUGUI livesText;

        public void DisplayScore(int score)
        {
            scoreText.text = score.ToString();
        }

        public void DisplayLives(int lives)
        {
            livesText.text = $"Lives: {lives}";
        }
    }
}