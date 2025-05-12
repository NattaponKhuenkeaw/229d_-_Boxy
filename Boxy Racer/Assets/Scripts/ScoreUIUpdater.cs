using UnityEngine;
using TMPro;

public class ScoreUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        scoreText.text = gameManager.score.ToString();
    }
}

