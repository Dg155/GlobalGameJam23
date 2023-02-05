using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public GameObject vineWarning;
    public GameObject leafAttack;
    public GameObject logWarning;
    private Score scoreManager;
    private GridSpawner grid;
    private int x;
    private int y;

    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridSpawner>();
        scoreManager = GameObject.FindWithTag("Score").GetComponent<Score>();
        InvokeRepeating("onebyone", 1, 1f);
        InvokeRepeating("threebyone", 2, 4f);
        InvokeRepeating("onebythree", 4, 5f);
        InvokeRepeating("sevenbyone", 8, 8f);
        InvokeRepeating("notonebythree", 15, 15f);
    }
    
    public void onebyone()
    {
        x = Random.Range(0, grid.gridCols);
        y = Random.Range(0, grid.gridRows);
        Instantiate(vineWarning, grid.returnGridPosition(x, y), Quaternion.identity);
        scoreManager.AddScore(50);
    }

    public void threebyone(){
        x = Random.Range(0, grid.gridCols-2);
        y = Random.Range(0, grid.gridRows);
        Instantiate(vineWarning, grid.returnGridPosition(x, y), Quaternion.identity);
        Instantiate(vineWarning, grid.returnGridPosition(x+1, y), Quaternion.identity);
        Instantiate(vineWarning, grid.returnGridPosition(x+2, y), Quaternion.identity);
    }

    public void onebythree(){
        x = Random.Range(0, grid.gridCols);
        Instantiate(leafAttack, grid.returnGridPosition(x, 0) + new Vector3(0, 0, -10), Quaternion.identity);
    }

    public void sevenbyone(){
        y = Random.Range(0, grid.gridRows);
        Instantiate(logWarning, grid.returnGridPosition(3, y) + new Vector3(0, 0, -5), Quaternion.identity);
    }

    public void notonebythree(){
        List<int> xList = new List<int> {0, 1, 2, 3, 4, 5, 6};
        x = Random.Range(0, grid.gridCols);
        xList.Remove(x);
        foreach (var x in xList)
        {
            Instantiate(leafAttack, grid.returnGridPosition(x, 0) + new Vector3(0, 0, -10), Quaternion.identity);
        }
    }
}
