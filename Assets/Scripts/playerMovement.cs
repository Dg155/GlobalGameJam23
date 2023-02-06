using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class playerMovement : MonoBehaviour
{

    private GridSpawner grid;
    private Score scoreManager;
    public float attackBufferTime = 0.5f;
    private bool attackBuffer = false;
    public int playerX = 3;
    public int playerY = 1;
    public AudioClip attackedSFX;
    public AudioClip attackedTreeSFX;
    public AudioClip movedSFX;
    private Animator animator;

    public GameObject[] Targets;

    int bossHealth = 5;

    public GameObject WinScreen;

    public GameObject TreeHurt;

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
            GameObject.FindWithTag("SFXPlayer").GetComponent<AudioSource>().PlayOneShot(movedSFX, 0.5f);
            playerX--;
            setPlayerPos(playerX, playerY);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (playerX >= grid.gridCols - 1) { return; }
            GameObject.FindWithTag("SFXPlayer").GetComponent<AudioSource>().PlayOneShot(movedSFX, 0.5f);
            playerX++;
            setPlayerPos(playerX, playerY);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (playerY >= grid.gridRows - 1) { return; }
            GameObject.FindWithTag("SFXPlayer").GetComponent<AudioSource>().PlayOneShot(movedSFX, 0.5f);
            playerY++;
            setPlayerPos(playerX, playerY);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (playerY <= 0) { return; }
            GameObject.FindWithTag("SFXPlayer").GetComponent<AudioSource>().PlayOneShot(movedSFX, 0.5f);
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
        if(collision.tag == "Axe")
        {
            AxeAttack(collision.gameObject);
            //Destroy(collision.gameObject);
        }

        if(collision.tag == "Attack" && !attackBuffer)
        {
            scoreManager.AddScore(-500);
            GameObject.FindWithTag("SFXPlayer").GetComponent<AudioSource>().PlayOneShot(attackedSFX);
            attackBuffer = true;
            animator.SetBool("startBuffer", true);
        }
    }
    void AxeAttack(GameObject Axe)
    {
        bossHealth--;
        StartCoroutine(MoveAxe(0, Axe));
        if (bossHealth == 0)
        {
            Time.timeScale = 0;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            WinScreen.SetActive(true);
            Debug.Log("Boss dead");
        }
       
    }
    
    IEnumerator MoveAxe(float delayTime, GameObject axe)
    {
        float animTime = 1.0f;
        Vector2 startPostion = new Vector2(axe.transform.position.x, axe.transform.position.y);
        Debug.Log("Start Coroutine");

        GameObject curTarget = Targets[Targets.Length - bossHealth - 1];
        Debug.Log(Targets.Length - bossHealth - 1);
        float elapsedTime = 0;
        while (elapsedTime < animTime)
        {
            Debug.Log(elapsedTime / animTime);
            axe.GetComponent<Animator>().enabled = true;
            axe.transform.position = Vector3.Lerp(startPostion, curTarget.transform.position, elapsedTime / animTime);
            elapsedTime += Time.deltaTime;
            yield return null;
            //axe.GetComponent<Animator>().enabled = false;
        }
        yield return new WaitForSeconds(animTime);
        GameObject.FindWithTag("SFXPlayer").GetComponent<AudioSource>().PlayOneShot(attackedTreeSFX);
        GameObject chips = Instantiate(TreeHurt, curTarget.transform.position, Quaternion.identity);
        axe.GetComponent<Animator>().enabled = false;

    }
}
