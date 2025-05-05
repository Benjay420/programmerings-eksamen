using UnityEngine;

public class StartScreenSpawner : MonoBehaviour
{
    public GameObject tilePrefab;
    public float spawnInterval = 0.5f;
    public float spawnWidth = 1.0f;

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
            Debug.LogError("Tile Prefab not assigned in StartScreenSpawner!", this);
            return;
        }

        float minX = transform.position.x - spawnWidth / 2.0f;
        float maxX = transform.position.x + spawnWidth / 2.0f;
        float randomX = Random.Range(minX, maxX);

        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

        Instantiate(tilePrefab, spawnPosition, Quaternion.identity);
    }
}