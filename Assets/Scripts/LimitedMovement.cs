using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedMovement : MonoBehaviour
{
    public float[] positions = { -6, -4, -2, 0, 2, 4, 6 }; // possible positions
    private int currentIndex = 3; // start at the center position (0)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex--;
            currentIndex = Mathf.Clamp(currentIndex, 0, 6);
            transform.position = new Vector3(positions[currentIndex], transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex++;
            currentIndex = Mathf.Clamp(currentIndex, 0, 6);
            transform.position = new Vector3(positions[currentIndex], transform.position.y, transform.position.z);
        }
    }
}