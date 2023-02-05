using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSpawner : MonoBehaviour
{
    public AudioClip rootSFX;
    public GameObject attack;
    
    public void spawnWarning()
    {
        Instantiate(attack, this.transform.position, Quaternion.identity);
        GameObject.FindWithTag("SFXPlayer").GetComponent<AudioSource>().PlayOneShot(rootSFX, 0.5f);
        Destroy(this.gameObject);
    }
}
