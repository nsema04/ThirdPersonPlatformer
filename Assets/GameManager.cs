using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public TextMeshProUGUI scoreText; 
    private int score = 0; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void IncrementScore()
    {
        score++; 
        scoreText.text = "Score: " + score;
    }
}
