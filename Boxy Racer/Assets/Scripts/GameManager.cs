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

    void Start()
    {
        gameOverPanel.SetActive(false);
        gameOverPanel.SetActive(false);
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
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

}
