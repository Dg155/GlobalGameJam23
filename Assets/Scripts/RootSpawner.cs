using UnityEngine;

public class RootSpawner : MonoBehaviour
{
    public GameObject rootPrefab; // the root prefab to spawn
    public float[] horizontalLanes = new float[7]; // array of horizontal lanes
    public float[] verticalLanes = new float[7]; // array of vertical lanes
    private float spawnInterval = 1.5f; // time interval between each spawn
    private float timer = 0.0f; // timer to keep track of time
    private int lastRandomLane = -1; // keeps track of the last random lane selected

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            int randomLane = Random.Range(0, 7); // choose a random lane
            // ensure that the new random lane is different from the last one
            while (randomLane == lastRandomLane)
            {
                randomLane = Random.Range(0, 7);
            }
            lastRandomLane = randomLane; // update the last random lane

            int randomDirection = Random.Range(0, 2); // choose a random direction

            if (randomDirection == 0)
            {
                // spawn a root horizontally
                float x = horizontalLanes[randomLane];
                Vector3 spawnPosition = new Vector3(x, transform.position.y, 0);
                GameObject root = Instantiate(rootPrefab, spawnPosition, Quaternion.identity);
                root.transform.Rotate(0, 0, 90); // rotate the root 90 degrees
                Destroy(root, 1.0f);
            }
            else
            {
                // spawn a root vertically
                float y = verticalLanes[randomLane];
                Vector3 spawnPosition = new Vector3(transform.position.x, y, 0);
                GameObject root = Instantiate(rootPrefab, spawnPosition, Quaternion.identity);
                Destroy(root, 1.0f);
            }

            timer = 0.0f;
        }
    }
}