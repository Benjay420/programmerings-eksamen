using UnityEngine;

public class Tile : MonoBehaviour
{
    public float speed = 5f;

    [SerializeField] private AudioClip tapSound;
    [SerializeField] private float tapVolume = 1.0f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        if (tapSound != null)
        {
            AudioSource.PlayClipAtPoint(tapSound, transform.position, tapVolume);
        }
        else
        {
            Debug.LogWarning("Tap Sound AudioClip is not assigned on the Tile script!", this);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddPoint();
        }

        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        if (enabled)
        {
             if (gameObject != null)
             {
                 Destroy(gameObject);
             }
        }
    }
}