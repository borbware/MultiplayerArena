using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupZ
{
public class ShockWave : MonoBehaviour
{
    public int shooter;
    float force = 400f;
    public AudioSource audioUse;
    public AudioClip playerHit;
    // Start is called before the first frame update
    void Awake()
    {
        audioUse = gameObject.GetComponent<AudioSource>();
        
        // audioUse.PlayOneShot(playerHit, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("TAke hit!");
            //Debug.Log(audioUse);
            Player otherPlayer = other.GetComponent<Player>();
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (otherPlayer.player != shooter)
            {
            StageManager.instance.UIManagers[shooter - 1].AddScore(10);
            rb.AddForce(direction * force);
            audioUse.PlayOneShot(playerHit, 1f);
            }
        }
    }
}

}