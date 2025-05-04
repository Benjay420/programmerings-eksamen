using UnityEngine;
using UnityEngine.SceneManagement; // REQUIRED for loading scenes!

public class GameOverUI : MonoBehaviour
{
    [Tooltip("The exact name of your main game scene file (e.g., Main.unity)")]
    [SerializeField] private string mainGameSceneName = "Main"; // CHANGE "Main" if your scene is named differently!

    // This function will be called by the Button's OnClick event
    public void RestartGame()
    {
        // Check if the scene name is set
        if (string.IsNullOrEmpty(mainGameSceneName))
        {
            Debug.LogError("GameOverUI: Main Game Scene Name is not set in the Inspector!", this);
            return;
        }

        Debug.Log("Restarting game, loading scene: " + mainGameSceneName);
        // Load the main game scene
        SceneManager.LoadScene(mainGameSceneName);
    }

    // Optional: Add a Quit button functionality later if needed
    // public void QuitGame()
    // {
    //     Debug.Log("Quitting game...");
    //     Application.Quit();
    //     #if UNITY_EDITOR // Only run this code block in the editor
    //     UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in editor
    //     #endif
    // }
}