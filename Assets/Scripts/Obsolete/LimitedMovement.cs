using UnityEngine;

public class LimitedMovement : MonoBehaviour
{
    private float[] horizontalPositions = { -9, -6, -3, 0, 3, 6, 9 }; // possible horizontal positions
    private float[] verticalPositions = { -6, -4, -2, 0, 2, 4, 6 }; // possible vertical positions
    private int currentHorizontalIndex = 3; // start at the center horizontal position (0)
    private int currentVerticalIndex = 0; // start at the center vertical position (0)

    public Transform respawnPoint; // the location where the player will respawn
    private int playerHealth = 3; // starting health of the player
    private int playerLives = 3; // total number of lives the player has

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentHorizontalIndex--;
            currentHorizontalIndex = Mathf.Clamp(currentHorizontalIndex, 0, 6);
            transform.localPosition = new Vector3(horizontalPositions[currentHorizontalIndex], transform.localPosition.y, transform.localPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentHorizontalIndex++;
            currentHorizontalIndex = Mathf.Clamp(currentHorizontalIndex, 0, 6);
            transform.localPosition = new Vector3(horizontalPositions[currentHorizontalIndex], transform.localPosition.y, transform.localPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentVerticalIndex++;
            currentVerticalIndex = Mathf.Clamp(currentVerticalIndex, 0, 6);
            transform.localPosition = new Vector3(transform.localPosition.x, verticalPositions[currentVerticalIndex], transform.localPosition.z);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentVerticalIndex--;
            currentVerticalIndex = Mathf.Clamp(currentVerticalIndex, 0, 6);
            transform.localPosition = new Vector3(transform.localPosition.x, verticalPositions[currentVerticalIndex], transform.localPosition.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Root"))
        {
            playerHealth--;
            Debug.Log("Hurt");
            if (playerHealth <= 0)
            {
                playerLives--;
                if (playerLives > 0)
                {
                    // Respawn the player
                    playerHealth = 3;
                    transform.position = respawnPoint.position;
                }
                else
                {
                    // Player is dead
                    // Add your code here to handle game over
                }
            }
        }
    }
}