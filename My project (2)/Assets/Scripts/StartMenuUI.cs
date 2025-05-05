using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private string mainGameSceneName = "Main";

    public void StartGame()
    {
        if (string.IsNullOrEmpty(mainGameSceneName))
        {
            Debug.LogError("StartMenuUI: Main Game Scene Name is not set in the Inspector!", this);
            return;
        }

        Debug.Log("Start button pressed. Loading scene: " + mainGameSceneName);
        SceneManager.LoadScene(mainGameSceneName);
    }
}