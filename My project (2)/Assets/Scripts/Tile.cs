using UnityEngine;

public class Tile : MonoBehaviour
{
    [Tooltip("Fall speed in units per second")]
    public float speed = 5f;

    void Update()
    {
        // Move tile downwards every frame
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        // Called when user clicks/taps the tile
        GameManager.Instance.AddPoint();
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        // If tile moves off-screen (bottom), destroy to avoid memory leak
        Destroy(gameObject);
    }
}
