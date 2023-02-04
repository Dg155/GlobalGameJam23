using System.Collections.Generic;
using UnityEngine;

public class SnakeRoots : MonoBehaviour
{
    public float speed = 2f;
    public float distanceBetweenRoots = 0.5f;
    public float timeBetweenRoots = 1f;
    public GameObject rootPrefab;

    private List<Transform> roots = new List<Transform>();
    private Vector3 direction = Vector3.right;
    private float timeSinceLastRoot = 0f;
    private bool isMoving = false;
    private Vector3 spawnPosition = Vector3.zero;

    private void Start()
    {
        Transform firstRoot = Instantiate(rootPrefab, transform).transform;
        roots.Add(firstRoot);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) && direction != Vector3.down)
        {
            direction = Vector3.up;
            MoveRoots();
        }
        else if (Input.GetKey(KeyCode.S) && direction != Vector3.up)
        {
            direction = Vector3.down;
            MoveRoots();
        }
        else if (Input.GetKey(KeyCode.A) && direction != Vector3.right)
        {
            direction = Vector3.left;
            MoveRoots();
        }
        else if (Input.GetKey(KeyCode.D) && direction != Vector3.left)
        {
            direction = Vector3.right;
            MoveRoots();
        }

        if (Input.anyKey)
        {
            timeSinceLastRoot += Time.deltaTime;
            if (timeSinceLastRoot >= timeBetweenRoots)
            {
                AddRoot();
                timeSinceLastRoot = 0f;
            }
        }
    }

    private void MoveRoots()
    {
        Vector3 previousRootPosition = roots[0].position;
        roots[0].position += direction * speed * Time.deltaTime;

        for (int i = 1; i < roots.Count; i++)
        {
            Transform currentRoot = roots[i];
            Vector3 currentRootPosition = currentRoot.position;

            currentRoot.position = previousRootPosition - (direction * distanceBetweenRoots);
            previousRootPosition = currentRootPosition;
        }
    }


    private void AddRoot()
    {
        Transform previousRoot = roots[roots.Count - 1];
        Transform newRoot = Instantiate(rootPrefab, previousRoot.position, Quaternion.identity).transform;
        roots.Add(newRoot);
    }
}