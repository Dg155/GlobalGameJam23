using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attacks : MonoBehaviour
{
  
    public GameObject gridObject;
    public GameObject oneSquareHit;
    private GridSpawner grid;
    private Vector3 spawnLocation;
    private int x;
    private int y;

    void Start()
    {
     gridObject = GameObject.FindGameObjectWithTag("Grid");
        onebyone();
        onebyone();
        onebyone();
        onebyone();
    }
    
    public void onebyone()
    {
        
        grid=gridObject.GetComponent<GridSpawner>();
        x = Random.Range(0, grid.gridCols);
        y = Random.Range(0, grid.gridRows);
        spawnLocation = grid.returnGridPosition(x, y);
        Instantiate(oneSquareHit, spawnLocation, Quaternion.identity);
        Destroy(gameObject, 1.0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
