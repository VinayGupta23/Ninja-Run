using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public static bool gameOver = false;
    public static int playerScore = 0;
    public static int highScore = 0;

    private static GameObject gameOverPanel;
    private static GameObject gameStartPanel;
    private static bool gameOverShown = false;

    Text scoreBox, highScoreBox;

    public void StartClick()
    {
        Time.timeScale = 1.0f;
        gameStartPanel.SetActive(false);
    }

    public void ExitClick()
    {
        Application.Quit();
    }
    
    public void RestartClick()
    {
        var cur = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(cur);
        SceneManager.LoadScene(cur);
        Time.timeScale = 1f;

        gameOver = false;
        gameOverShown = false;
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("highScore"))
            PlayerPrefs.SetInt("highScore", 0);

        Time.timeScale = 0;
    }

    // Use this for initialization
    void Start () {
        gameOverPanel = GameObject.FindGameObjectWithTag("Finish");
        gameOverPanel.SetActive(false);
        gameStartPanel = GameObject.FindGameObjectWithTag("Start");
        gameStartPanel.SetActive(true);

        scoreBox = GameObject.Find("ScoreBox").GetComponent<Text>();
        highScoreBox = GameObject.Find("HighScoreBox").GetComponent<Text>();

        highScore = PlayerPrefs.GetInt("highScore");
        playerScore = 0;
        scoreBox.text = "Score: " + playerScore.ToString();
        highScoreBox.text = "High score: " + highScore.ToString();
    }

    // Update is called once per frame
    void Update() {
        if (gameOver)
        {
            highScore = Mathf.Max(playerScore, highScore);
            PlayerPrefs.SetInt("highScore", highScore);

            Time.timeScale = 0f;
            if (!gameOverShown)
            {
                gameOverPanel.SetActive(true);
                gameOverShown = true;
            }
        }

        scoreBox.text = "Score: " + playerScore.ToString();
    }
}