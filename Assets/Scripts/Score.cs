using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private int score;

    private void Start()
    {
        score = 1000;
    }

    public int getScore()
    {
        return score;
    }

    private void Update()
    {
        if(score <= 0)
        {
            //DIE
        }
    }
    public void AddScore(int x)
    {
        score += x;
    }
}
