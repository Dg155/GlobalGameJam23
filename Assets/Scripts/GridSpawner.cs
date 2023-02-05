using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridSpawner : MonoBehaviour
{

    public GameObject Player;
    public int gridRows = 3;
    public int gridCols = 7;
    public float gridSpacing = 2.6f;
    public Grid grid;

    // Start is called before the first frame update
    private void Start()
    {
        makeGrid();
    }

    private void makeGrid(){
        grid = new Grid(gridCols, gridRows, gridSpacing, new Vector3(-9.88f, -4.71f));
    }

    public Vector3 returnGridPosition(int x, int y) {
        if (grid == null) {
            makeGrid();
        }
        return grid.playerPosition(x, y);
    }

}
