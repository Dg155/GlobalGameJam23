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
            MoveHead();
        }
        else if (Input.GetKey(KeyCode.S) && direction != Vector3.up)
        {
            direction = Vector3.down;
            MoveHead();
        }
        else if (Input.GetKey(KeyCode.A) && direction != Vector3.right)
        {
            direction = Vector3.left;
            MoveHead();
        }
        else if (Input.GetKey(KeyCode.D) && direction != Vector3.left)
        {
            direction = Vector3.right;
            MoveHead();
        }

        timeSinceLastRoot += Time.deltaTime;
        if (timeSinceLastRoot >= timeBetweenRoots)
        {
            AddRoot();
            timeSinceLastRoot = 0f;
        }
    }

    private void MoveHead()
    {
        Transform firstRoot = roots[0];
        firstRoot.position += direction * speed * Time.deltaTime;

        Vector3 previousRootPosition = firstRoot.position;
        for (int i = 1; i < roots.Count; i++)
        {
            Transform currentRoot = roots[i];
            currentRoot.position = previousRootPosition - (direction * distanceBetweenRoots);
            previousRootPosition = currentRoot.position;
        }
    }

    private void AddRoot()
    {
        Transform previousRoot = roots[roots.Count - 1];
        Transform newRoot = Instantiate(rootPrefab, previousRoot.position, Quaternion.identity).transform;
        roots.Add(newRoot);
    }
}