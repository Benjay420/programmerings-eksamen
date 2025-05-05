using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private string mainGameSceneName = "Main";

    public void RestartGame()
    {
        if (string.IsNullOrEmpty(mainGameSceneName))
        {
            Debug.LogError("GameOverUI: Main Game Scene Name is not set in the Inspector!", this);
            return;
        }
        Debug.Log("Restarting game, loading scene: " + mainGameSceneName);
        SceneManager.LoadScene(mainGameSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button pressed. Attempting to quit application...");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}