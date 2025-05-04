using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Only needed if you add restart functionality later

public class GameManager : MonoBehaviour
{
    // --- Singleton Pattern ---
    public static GameManager Instance { get; private set; }

    // --- Inspector Variables ---
    [Header("UI Elements")]
    [Tooltip("Assign the TextMeshPro UI element used to display the score.")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Tile Spawner Control")]
    [Tooltip("Reference to the TileSpawner in the scene.")]
    [SerializeField] private TileSpawner tileSpawner; // Assign this in the Inspector!

    [Header("Game Difficulty Settings - Spawn Rate")]
    [Tooltip("Initial time between tile spawns (seconds). Lower is faster.")]
    [SerializeField] private float initialSpawnInterval = 1.0f;
    [Tooltip("Fastest possible time between spawns (seconds).")]
    [SerializeField] private float minSpawnInterval = 0.2f;
    [Tooltip("How much to decrease spawn interval by each difficulty step.")]
    [SerializeField] private float intervalDecreaseAmount = 0.1f;

    // --- NEW: Difficulty Settings - Tile Speed ---
    [Header("Game Difficulty Settings - Tile Speed")]
    [Tooltip("Initial speed at which tiles fall (units per second).")]
    [SerializeField] private float initialTileSpeed = 5.0f;
    [Tooltip("Maximum speed tiles will reach (units per second).")]
    [SerializeField] private float maxTileSpeed = 15.0f;
    [Tooltip("How much to increase tile speed by each difficulty step.")]
    [SerializeField] private float speedIncreaseAmount = 0.5f;
    // --- END NEW ---

    [Header("Game Difficulty Settings - Timing")]
    [Tooltip("How many seconds between each difficulty increase step.")]
    [SerializeField] private float timeToIncreaseDifficulty = 10.0f;


    // --- Public Properties ---
    // Allows TileSpawner to ask the GameManager for the current target speed
    public float CurrentTileSpeed { get; private set; } // --- NEW ---

    // --- Private Variables ---
    private int score = 0;
    private float currentSpawnInterval;
    // Removed: private TileSpawner tileSpawner; // Now assigned via Inspector
    private float timeSinceLastDifficultyIncrease = 0f;


    // --- Unity Methods ---
    void Awake()
    {
        // Setup Singleton
        if (Instance == null) {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Optional
        } else {
            Destroy(gameObject);
            return;
        }

        // --- REMOVED FindObjectOfType ---
        // We now assign the TileSpawner via the Inspector. Add a check:
        if (tileSpawner == null) {
            Debug.LogError("ERROR: TileSpawner is not assigned in the GameManager Inspector!", this);
            // Disable the component to prevent errors if the spawner is missing
             enabled = false;
             return;
        }
        // --- END REMOVED ---
    }

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        // Only run difficulty logic if the spawner was assigned
        // (The 'enabled = false' in Awake handles the null case)
        IncreaseDifficultyOverTime();
    }

    // --- Public Methods ---
    public void AddPoint()
    {
        score++;
        UpdateScoreText();
    }

    // --- Private Helper Methods ---
    private void InitializeGame()
    {
        score = 0;
        timeSinceLastDifficultyIncrease = 0f;

        // Reset spawn interval
        currentSpawnInterval = initialSpawnInterval;
        tileSpawner.spawnInterval = currentSpawnInterval; // Set initial interval on the spawner

        // --- NEW: Reset tile speed ---
        CurrentTileSpeed = initialTileSpeed; // Set initial speed property
        // --- END NEW ---

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
            timeSinceLastDifficultyIncrease = 0f; // Reset timer

            // Calculate new spawn interval
            float potentialNewInterval = currentSpawnInterval - intervalDecreaseAmount;
            float newInterval = Mathf.Max(minSpawnInterval, potentialNewInterval);

            // Check if difficulty actually changed (interval decreased)
            if (newInterval < currentSpawnInterval)
            {
                // Update Spawn Interval
                currentSpawnInterval = newInterval;
                tileSpawner.spawnInterval = currentSpawnInterval;

                // --- NEW: Update Tile Speed ---
                float potentialNewSpeed = CurrentTileSpeed + speedIncreaseAmount;
                // Use Mathf.Min because speed INCREASES UP TO a max value
                CurrentTileSpeed = Mathf.Min(maxTileSpeed, potentialNewSpeed);
                // --- END NEW ---

                Debug.Log("Difficulty Increased! New Spawn Interval: " + currentSpawnInterval + ", New Tile Speed: " + CurrentTileSpeed);
            }
        }
    }
}