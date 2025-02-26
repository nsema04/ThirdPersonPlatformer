using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public TextMeshProUGUI scoreText; // UI text element
    private int score = 0; // Player's score

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void IncrementScore()
    {
        score++; // Increase score
        scoreText.text = "Score: " + score; // Update UI
    }
}
