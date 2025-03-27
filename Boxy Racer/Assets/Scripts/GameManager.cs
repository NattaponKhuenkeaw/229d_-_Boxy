using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }
    public  void AddScore(int score1)
    {
        score += score1;
        scoreText.text = this.score.ToString();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
