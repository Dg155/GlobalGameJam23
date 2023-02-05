using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Axe : MonoBehaviour
{
    public GameObject axeItem;
    private GridSpawner grid;
    private int x;
    private int y;

    private void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridSpawner>();
        InvokeRepeating("axeSpawned", 1, 20f);
    }

    public void axeSpawned()
    {
        x = Random.Range(0, grid.gridCols);
        y = Random.Range(0, grid.gridRows);
        Instantiate(axeItem, grid.returnGridPosition(x, y), Quaternion.identity);
    }
}
