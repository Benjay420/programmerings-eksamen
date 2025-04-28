using UnityEngine;

using TMPro; // only if using TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TMP_Text scoreText; // or UnityEngine.UI.Text if not using TMP

    private int score = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}