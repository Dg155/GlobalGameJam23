using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{

    void Start()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EnableCollider()
    {
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void EndMe()
    {
        Destroy(gameObject);
    }
}
