using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    // Correct function name and parameter type?
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Add this line for debugging:
        Debug.Log("Trigger Entered by: " + otherCollider.name + " with tag: " + otherCollider.tag);

        // Correct CompareTag check (case-sensitive)?
        if (otherCollider.CompareTag("Tile"))
        {
            Debug.Log("Tile Tag Confirmed - Loading GameOverScene!");
            Destroy(otherCollider.gameObject);
            SceneManager.LoadScene("GameOverScene");
        }
    }
}