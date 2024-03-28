using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BallController ballScript;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI congratsText;
    [SerializeField] private GameObject brickPrefab;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    public GameObject congratsParticlePrefab;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        isGameActive = true;
        ballScript.launched = false;
        score = 0;
        UpdateScore(score);
        audioSource.clip = backgroundMusic;
        audioSource.Play();
        SpawnBricks();
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
        scoreText.text = "Score: " + score;
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
}
