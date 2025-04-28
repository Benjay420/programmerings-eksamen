using UnityEngine;

public class TileSpawner : MonoBehaviour {
    public GameObject tilePrefab;
    public float spawnInterval = 1f;
    void Start() {
        InvokeRepeating(nameof(SpawnTile), 1f, spawnInterval);
    }
    void SpawnTile() {
        float x = Random.Range(-2f, 2f);
        Vector3 pos = new Vector3(x, 6f, 0f);
        Instantiate(tilePrefab, pos, Quaternion.identity);
    }
}
