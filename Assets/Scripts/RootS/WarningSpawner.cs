using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningSpawner : MonoBehaviour
{
    public AudioClip rootSFX;
    public GameObject attack;
    
    public void spawnWarning()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2) {Destroy(this.gameObject); return;}
        if (rootSFX.name == "LeafCrumple") {
            Instantiate(attack, this.transform.position + new Vector3(0.3f, 0, 0), Quaternion.identity);
        }
        else {Instantiate(attack, this.transform.position, Quaternion.identity);}
        if (rootSFX.name == "Crunch"){
            GameObject.FindWithTag("SFXPlayer").GetComponent<AudioSource>().PlayOneShot(rootSFX, 0.5f);
        }
        else{
            GameObject.FindWithTag("SFXPlayer").GetComponent<AudioSource>().PlayOneShot(rootSFX, 1f);
        }
        Destroy(this.gameObject);
    }
}
