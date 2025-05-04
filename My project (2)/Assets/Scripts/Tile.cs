using UnityEngine;

// No longer require AudioSource on this object itself!
// [RequireComponent(typeof(AudioSource))] // <<< REMOVE OR COMMENT OUT THIS LINE

public class Tile : MonoBehaviour
{
    [Tooltip("Fall speed in units per second")]
    public float speed = 5f;

    // --- NEW ---
    [Header("Audio Settings")]
    [Tooltip("The sound clip to play when tapped.")]
    [SerializeField] private AudioClip tapSound; // Assign this in the Inspector!

    [Tooltip("Volume for the tap sound.")]
    [Range(0f, 1f)] // Creates a slider in the Inspector
    [SerializeField] private float tapVolume = 1.0f;

    // --- REMOVED ---
    // We don't need a reference to an AudioSource on this specific tile anymore
    // private AudioSource audioSrc;

    // Awake is no longer needed for getting the AudioSource
    // void Awake() { ... } // <<< REMOVE OR COMMENT OUT THE Awake() FUNCTION

    void Update()
    {
        // Move tile downwards every frame based on speed and time
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        // --- CHANGE: Use PlayClipAtPoint ---
        if (tapSound != null)
        {
            // Play the assigned sound clip AT the tile's current position
            // Uses the main AudioListener (usually on the Camera) to determine volume/panning
            AudioSource.PlayClipAtPoint(tapSound, transform.position, tapVolume);
        }
        else
        {
             Debug.LogWarning("Tap Sound AudioClip is not assigned on the Tile script!", this);
        }

        // --- Score Update (Keep this) ---
        if (GameManager.Instance != null)
        {
             GameManager.Instance.AddPoint();
        }

        // --- CHANGE: Destroy Immediately ---
        // Destroy the tile GameObject instantly now.
        // The sound was already triggered via PlayClipAtPoint and will play independently.
        Destroy(gameObject); // <<< REMOVE THE DELAY ( , 0.1f )
    }

    void OnBecameInvisible()
    {
        // This function remains the same
        if (enabled)
        {
            if (gameObject != null)
            {
                 Destroy(gameObject);
            }
        }
    }
}