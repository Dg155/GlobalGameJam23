using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    private GridSpawner grid;
    private Score scoreManager;
    public float attackBufferTime = 0.5f;
    private bool attackBuffer = false;
    public int playerX = 3;
    public int playerY = 1;
    public AudioClip attackedSFX;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        scoreManager = GameObject.FindWithTag("Score").GetComponent<Score>();
        grid =  GameObject.FindWithTag("Grid").GetComponent<GridSpawner>();
        setPlayerPos(playerX, playerY);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (playerX <= 0) { return;}
            playerX--;
            setPlayerPos(playerX, playerY);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (playerX >= grid.gridCols - 1) { return; }
            playerX++;
            setPlayerPos(playerX, playerY);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (playerY >= grid.gridRows - 1) { return; }
            playerY++;
            setPlayerPos(playerX, playerY);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (playerY <= 0) { return; }
            playerY--;
            setPlayerPos(playerX, playerY);
        }
    }

    private void setPlayerPos(int x, int y)
    {
        transform.position = grid.returnGridPosition(x, y);
    }

    public void resetAttackBuffer()
    {
        attackBuffer = false;
        animator.SetBool("startBuffer", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Attack" && !attackBuffer)
        {
            scoreManager.AddScore(-100);
            GameObject.FindWithTag("SFXPlayer").GetComponent<AudioSource>().PlayOneShot(attackedSFX);
            attackBuffer = true;
            animator.SetBool("startBuffer", true);
        }
    }
}
