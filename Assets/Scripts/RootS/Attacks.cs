using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class Attacks : MonoBehaviour
{
    public GameObject vineWarning;
    public GameObject leafWarning;
    public GameObject logWarning;
    private Score scoreManager;
    private GridSpawner grid;
    private int x;
    private int y;

    void Start()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridSpawner>();
        scoreManager = GameObject.FindWithTag("Score").GetComponent<Score>();
        foreach (GameObject warning in GameObject.FindGameObjectsWithTag("Warning")){
            Destroy(warning);
        }
        StartWaves();
    }

    private async Task StartWaves(){
        await Wave(1);
        Debug.Log("Wave 1 complete");
        if (SceneManager.GetActiveScene().buildIndex != 2){return;}
        await Wave(2);
        Debug.Log("Wave 2 complete");
        if (SceneManager.GetActiveScene().buildIndex != 2){return;}
        await Wave(3);
        Debug.Log("Wave 3 complete");
    }

    private async Task Wave(int waveNumber){
        Debug.Log("Wave "+ waveNumber + " started");
        var TaskList = new List<Task>();
        if (waveNumber == 1){
            TaskList.Add(randomlySpawnAttack("onebyone", 15, 1f, 2f));
            TaskList.Add(randomlySpawnAttack("threebyone", 5, 3, 5f));
            TaskList.Add(randomlySpawnAttack("sevenbyone", 4, 4f, 6f));
            TaskList.Add(randomlySpawnAttack("onebythree", 4, 4f, 6f));
        }
        if (waveNumber == 2){
            TaskList.Add(randomlySpawnAttack("onebyone", 30, 1f, 1.8f));
            TaskList.Add(randomlySpawnAttack("threebyone", 10, 2f, 5f));
            TaskList.Add(randomlySpawnAttack("sevenbyone", 8, 3f, 6f));
            TaskList.Add(randomlySpawnAttack("onebythree", 8, 3f, 6f));
            TaskList.Add(randomlySpawnAttack("notonebythree", 2, 10f, 20f));
        }
        if (waveNumber == 3){    
            TaskList.Add(randomlySpawnAttack("onebyone", 50, 0.5f, 1f));
            TaskList.Add(randomlySpawnAttack("threebyone", 27, 1.5f, 2f));
            TaskList.Add(randomlySpawnAttack("sevenbyone", 12, 3f, 5f));
            TaskList.Add(randomlySpawnAttack("onebythree", 12, 3f, 5f));
            TaskList.Add(randomlySpawnAttack("notonebythree", 4, 10f, 15f));
        }
        await Task.WhenAll(TaskList);
    }

    private async Task randomlySpawnAttack(string attackname, int attackAmount, float attackDelaymin, float attackDelayMax){
        float spawnInterval;
        float timeBefore;
        for (int i = 0; i < attackAmount; i++)
        {
            if (SceneManager.GetActiveScene().buildIndex != 2){return;}
            spawnInterval = Random.Range(attackDelaymin, attackDelayMax);
            timeBefore = 0f;
            while (timeBefore < spawnInterval) {timeBefore += Time.deltaTime; await Task.Yield();}
            switch (attackname)
            {
                case "onebyone":
                    onebyone();
                    break;
                case "threebyone":
                    threebyone();
                    break;
                case "onebythree":
                    onebythree();
                    break;
                case "sevenbyone":
                    sevenbyone();
                    break;
                case "notonebythree":
                    notonebythree();
                    break;
            }
        }
    }
    
    public void onebyone()
    {
        x = Random.Range(0, grid.gridCols);
        y = Random.Range(0, grid.gridRows);
        Instantiate(vineWarning, grid.returnGridPosition(x, y) + new Vector3(0, 0, -5), Quaternion.identity);
        scoreManager.AddScore(10);
    }

    public void threebyone(){
        x = Random.Range(0, grid.gridCols-2);
        y = Random.Range(0, grid.gridRows);
        Instantiate(vineWarning, grid.returnGridPosition(x, y) + new Vector3(0, 0, -5), Quaternion.identity);
        Instantiate(vineWarning, grid.returnGridPosition(x+1, y) + new Vector3(0, 0, -5), Quaternion.identity);
        Instantiate(vineWarning, grid.returnGridPosition(x+2, y) + new Vector3(0, 0, -5), Quaternion.identity);
        scoreManager.AddScore(25);
    }

    public void onebythree(){
        x = Random.Range(0, grid.gridCols);
        Instantiate(leafWarning, grid.returnGridPosition(x, 0) + new Vector3(1f, 0, -5), Quaternion.identity);
        scoreManager.AddScore(50);
    }

    public void sevenbyone(){
        y = Random.Range(0, grid.gridRows);
        Instantiate(logWarning, grid.returnGridPosition(3, y) + new Vector3(0, 0, -5), Quaternion.identity);
        scoreManager.AddScore(50);
    }

    public void notonebythree(){
        List<int> xList = new List<int> {0, 1, 2, 3, 4, 5, 6};
        x = Random.Range(0, grid.gridCols);
        xList.Remove(x);
        foreach (var x in xList)
        {
            Instantiate(leafWarning, grid.returnGridPosition(x, 0) + new Vector3(1f, 0, -5), Quaternion.identity);
        }
        scoreManager.AddScore(100);
    }
}
