using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokkaus : MonoBehaviour
{
    float pushforce = 7;
    Player player;
    Vector3 spawnlocation;
    AudioSource walking;
    [SerializeField] AudioClip slap;
    Rigidbody tisrigid;
    float audiocd = 0, dashspeed = 500000;
    void Start()
    {
        spawnlocation = transform.position;
        player = GetComponent<Player>();
        walking = GetComponent<AudioSource>();
        tisrigid = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        audiocd -= 1 * Time.deltaTime;
        if(tisrigid.velocity.sqrMagnitude > 1.4f)
        {
            if(audiocd < 0)
            {walking.Play(); audiocd = 1;}
            
        }
        else{walking.Stop();}
        if(Input.GetButtonDown("Fire2"))
        {
            Debug.Log("podwq");
            tisrigid.AddForce(transform.forward * Time.deltaTime * dashspeed);
        }
       
        
    }
    void OnTriggerEnter(Collider other) 
    {   
     
        GameObject toher = other.gameObject;
        if(toher.tag == "Player")
        {
            walking.PlayOneShot(slap);
            var pushother = toher.GetComponent<Rigidbody>();
            pushother.AddForce((toher.transform.position - gameObject.transform.position)
            .normalized * tisrigid.velocity.sqrMagnitude * pushforce);
        }    
        if(toher.tag == "fial")
        {
            if(player.UIManager.score == 0)
            {Destroy(gameObject);}
            else
            {player.UIManager.AddScore(-1); transform.position = spawnlocation;}
        }
    }
}
