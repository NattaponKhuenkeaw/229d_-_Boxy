using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextWin;
    public int score = 0;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public bool isGameStop = false;
    private bool isPaused = false;

    

    void Start()
    {
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
    }
    public  void AddScore(int score1)
    {
        score += score1;
        scoreText.text = this.score.ToString();
        scoreTextWin.text = this.score.ToString();
       
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void GameWin()
    {
        gameWinPanel.SetActive(true);
    }
    
    public void Restart()
    {
        
        SceneManager.LoadScene(1);
        TogglePause();
        
        
    }
    public void Quit()
    {
            Application.Quit(); 
            Debug.Log("Game is exiting..."); 
    }
    public void TogglePause()
    {
       
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            gameOverPanel.SetActive(isPaused);
    }

}
