using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    public float spawnInterval = 1.0f;
    [SerializeField] private float spawnRangeX = 2.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnTile();
            timer = 0f;
        }
    }

    void SpawnTile()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile Prefab not assigned in TileSpawner!", this);
            return;
        }

        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

        GameObject newTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);

        Tile tileScript = newTile.GetComponent<Tile>();
        if (tileScript != null)
        {
            if (GameManager.Instance != null)
            {
                tileScript.speed = GameManager.Instance.CurrentTileSpeed;
            }
            else
            {
                 Debug.LogWarning("Could not find GameManager.Instance to set tile speed.", this);
            }
        }
        else
        {
             Debug.LogError("Spawned object is missing the Tile script!", newTile);
        }
    }
}