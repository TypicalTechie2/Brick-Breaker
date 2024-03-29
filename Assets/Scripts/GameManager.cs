using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private BallController ballScript;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI congratsText;
    public GameObject brickPrefab;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    public GameObject congratsParticlePrefab;
    [SerializeField] Button restartButton;
    [SerializeField] Button ExitToMenuButton;
    private int rows = 5;
    private int columns = 5;
    private float brickWidth = 3f;
    private float brickHeight = 1f;
    private int score;
    public bool isGameActive;
    private int totalBricks;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ballScript = GameObject.Find("Ball").GetComponent<BallController>();
        StartGame();
        Debug.Log("Game Started");
    }

    void SpawnBricks()
    {
        totalBricks = rows * columns;
        Vector3 startPosition = new Vector3(-6, 1f, 0f);
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = startPosition + new Vector3(col * brickWidth, row * brickHeight, 0);
                Instantiate(brickPrefab, position, Quaternion.identity, transform);
            }
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        //scoreText.text = "Score: " + score;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void ResetGame()
    {
        score = 0;
        UpdateScoreUI();
    }

    public void BrickDestroyed()
    {
        totalBricks--;

        if (totalBricks <= 0)
        {
            congratsText.gameObject.SetActive(true);
            Instantiate(congratsParticlePrefab, Vector3.zero, Quaternion.identity);
            Destroy(ballScript.gameObject);
            isGameActive = false;
        }
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        audioSource.Stop();
    }
    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        SpawnBricks();
        UpdateScore(score);
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }

    public void RestartGame()
    {
        ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
