using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [Tooltip("The Tile Prefab to spawn.")]
    [SerializeField] private GameObject tilePrefab; // Assign this in Inspector

    [Tooltip("Time between spawns (seconds). Controlled by GameManager.")]
    public float spawnInterval = 1.0f; // GameManager will update this

    [Tooltip("Horizontal range (X) for spawning tiles.")]
    [SerializeField] private float spawnRangeX = 2.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnTile();
            timer = 0f; // Reset timer
        }
    }

    void SpawnTile()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile Prefab not assigned in TileSpawner!", this);
            return;
        }

        // Calculate random X position within the range
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

        // Instantiate the tile
        GameObject newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);

        // --- NEW: Set the speed on the spawned tile ---
        Tile tileScript = newTile.GetComponent<Tile>();
        if (tileScript != null)
        {
            // Check if GameManager exists before accessing it
            if (GameManager.Instance != null)
            {
                // Get the current speed from GameManager and apply it
                tileScript.speed = GameManager.Instance.CurrentTileSpeed;
            }
            else
            {
                 Debug.LogWarning("Could not find GameManager.Instance to set tile speed.", this);
                 // Tile will use its default speed defined in Tile.cs or prefab
            }
        }
        else
        {
             Debug.LogError("Spawned object is missing the Tile script!", newTile);
        }
        // --- END NEW ---
    }
}