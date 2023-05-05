using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupZ
{
   
public class DeathPlaneCollision : MonoBehaviour
{
     AudioSource audioUse;
    public AudioClip playerDies;

    void Start() 
    {
    audioUse = GetComponent<AudioSource>();  
    
    }
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Death")
        {
           // Destroy(gameObject, 3f);
           transform.GetChild(0).gameObject.SetActive(false);
           
           audioUse.PlayOneShot(playerDies, 1f);
            var _player = GetComponent<Player>();
            if (_player != null)
                StageManager.instance.AddHP(_player.player, -100);
        }
    }
}
}
