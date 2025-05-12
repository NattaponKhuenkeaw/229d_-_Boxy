using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText;
   // public TextMeshProUGUI scoreTextWin;
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
        //scoreTextWin.text = this.score.ToString();
       
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
    
    public void Restart()
    {
        
        SceneManager.LoadScene(1);
        
        
        
    }
    public void Quit()
    {
            Application.Quit(); 
            Debug.Log("Game is exiting..."); 
    }
   
}
