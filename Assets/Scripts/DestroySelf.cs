using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{

    public float destoryTime = 1.0f;

    public void EndMe()
    {
        Destroy(gameObject);
    }
}
