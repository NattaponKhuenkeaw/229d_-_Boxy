using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    
    public int score = 0;
    public int highScore ;

    public GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void AddScore(int score1)
    {
        score += score1;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            
        }
    }
    
    public void Restart()
    {
        
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
    }
}

