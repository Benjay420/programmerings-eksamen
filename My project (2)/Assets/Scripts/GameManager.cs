using UnityEngine;
using TMPro; // Use this if your score text is TextMeshPro
// using UnityEngine.UI; // Use this if your score text is legacy UI Text

public class GameManager : MonoBehaviour
{
    // --- Singleton Pattern ---
    // Provides a globally accessible instance of the GameManager
    public static GameManager Instance { get; private set; }

    // --- Inspector Variables ---
    [Header("UI Elements")]
    [Tooltip("Assign the TextMeshPro UI element used to display the score.")]
    [SerializeField] private TMP_Text scoreText; // Use 'Text' if using legacy UI Text

    // --- Private Variables ---
    private int score = 0;

    // --- Unity Methods ---

    void Awake()
    {
        // Set up the Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            // Optional: Keep GameManager alive even if we change scenes later
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another instance already exists, destroy this one to enforce the singleton
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Initialize game state when the scene starts (or restarts)
        InitializeGame();
    }

    // --- Public Methods ---

    // Called by Tiles when they are successfully clicked
    public void AddPoint()
    {
        score++;
        UpdateScoreText();
    }

    // --- Private Helper Methods ---

    private void InitializeGame()
    {
        score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            // Warn the developer if they forgot to link the text field in the editor
            Debug.LogWarning("GameManager: Score Text UI element is not assigned in the Inspector.", this);
        }
    }
}