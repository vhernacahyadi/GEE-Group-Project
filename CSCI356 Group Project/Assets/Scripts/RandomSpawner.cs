using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPrefab;

    [SerializeField]
    private float interval;

    [SerializeField]
    private float spawnRadius;

    [SerializeField]
    private float spawnHeight;

    [SerializeField]
    private int maxSpawn;

    private int currentCount;

    // Start is called before the first frame update
    void Start()
    {
        currentCount = 0;

        // Start spawning
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Spawn()
    {
        while (currentCount < maxSpawn)
        {
            // Debug
            //Debug.Log("Spawn");

            // Random position
            Vector2 randomPos2D = Random.insideUnitCircle * spawnRadius;
            Vector3 randomPos = new Vector3(randomPos2D.x, spawnHeight, randomPos2D.y) + transform.position;
            
            // Instantiate at random position
            GameObject newObject = Instantiate(spawnPrefab, randomPos, Quaternion.identity);

            // Increment count
            currentCount++;

            yield return new WaitForSeconds(interval);
        }
    }
}
