using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Debug.Log("Trigger Entered by: " + otherCollider.name + " with tag: " + otherCollider.tag);

        if (otherCollider.CompareTag("Tile"))
        {
            Debug.Log("Tile Tag Confirmed - Loading GameOverScene!");
            Destroy(otherCollider.gameObject);
            SceneManager.LoadScene("GameOverScene");
        }
    }
}