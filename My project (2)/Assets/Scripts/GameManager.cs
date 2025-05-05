using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TileSpawner tileSpawner;
    [SerializeField] private float initialSpawnInterval = 1.0f;
    [SerializeField] private float minSpawnInterval = 0.2f;
    [SerializeField] private float intervalDecreaseAmount = 0.1f;
    [SerializeField] private float initialTileSpeed = 5.0f;
    [SerializeField] private float maxTileSpeed = 15.0f;
    [SerializeField] private float speedIncreaseAmount = 0.5f;
    [SerializeField] private float timeToIncreaseDifficulty = 10.0f;

    public float CurrentTileSpeed { get; private set; }

    private int score = 0;
    private float currentSpawnInterval;
    private float timeSinceLastDifficultyIncrease = 0f;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        if (tileSpawner == null) {
            Debug.LogError("ERROR: TileSpawner is not assigned in the GameManager Inspector!", this);
             enabled = false;
             return;
        }
    }

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        IncreaseDifficultyOverTime();
    }

    public void AddPoint()
    {
        score++;
        UpdateScoreText();
    }

    private void InitializeGame()
    {
        score = 0;
        timeSinceLastDifficultyIncrease = 0f;
        currentSpawnInterval = initialSpawnInterval;
        tileSpawner.spawnInterval = currentSpawnInterval;
        CurrentTileSpeed = initialTileSpeed;
        UpdateScoreText();
        Debug.Log("Game Initialized. Spawn Interval: " + currentSpawnInterval + ", Tile Speed: " + CurrentTileSpeed);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null) {
            scoreText.text = "Score: " + score;
        } else {
            Debug.LogWarning("GameManager: Score Text UI element not assigned.", this);
        }
    }

    private void IncreaseDifficultyOverTime()
    {
        timeSinceLastDifficultyIncrease += Time.deltaTime;

        if (timeSinceLastDifficultyIncrease >= timeToIncreaseDifficulty)
        {
            timeSinceLastDifficultyIncrease = 0f;

            float potentialNewInterval = currentSpawnInterval - intervalDecreaseAmount;
            float newInterval = Mathf.Max(minSpawnInterval, potentialNewInterval);

            if (newInterval < currentSpawnInterval)
            {
                currentSpawnInterval = newInterval;
                tileSpawner.spawnInterval = currentSpawnInterval;

                float potentialNewSpeed = CurrentTileSpeed + speedIncreaseAmount;
                CurrentTileSpeed = Mathf.Min(maxTileSpeed, potentialNewSpeed);

                Debug.Log("Difficulty Increased! New Spawn Interval: " + currentSpawnInterval + ", New Tile Speed: " + CurrentTileSpeed);
            }
        }
    }
}