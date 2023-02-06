using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField]
    private int score;
    private TMPro.TextMeshProUGUI scoreText;
   

    public GameObject LoseScreen;
    public GameObject Player;

    private void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        score = 1000;
        scoreText.text = score.ToString();
    }

    public int getScore()
    {
        return score;
    }

    private void Update()
    {
        if(score <= 0)
        {
            Time.timeScale = 0;
            Player.SetActive(false);
            LoseScreen.SetActive(true);
        }
    }
    public void AddScore(int x)
    {
        score += x;
        scoreText.text = score.ToString();
    }
}
