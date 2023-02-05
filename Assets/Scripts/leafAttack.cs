using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leafAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(selfDestruct());
    }

    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
