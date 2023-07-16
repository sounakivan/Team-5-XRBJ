using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public float spawnInterval = 4f;

    private void Start()
    {
        // Start invoking the function to spawn the prefab with the specified interval
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
    }

    private void SpawnPrefab()
    {
        // Instantiate the prefab at the current position of this GameObject (spawner)
        Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
    }
}
