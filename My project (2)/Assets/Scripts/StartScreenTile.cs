using UnityEngine;

public class StartScreenTile : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        if (enabled && gameObject != null)
        {
             Destroy(gameObject);
        }
    }
}