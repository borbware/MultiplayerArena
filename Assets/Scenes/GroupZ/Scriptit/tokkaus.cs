using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokkaus : MonoBehaviour
{
    float pushforce = 7;
    Player player;
    Vector3 spawnlocation;
    AudioSource slap,walkingdownthestreet;
    Rigidbody tisrigid;
    float audiocd = 0;
    void Start()
    {
        walkingdownthestreet = GetComponent<AudioSource>();
        spawnlocation = transform.position;
        player = GetComponent<Player>();
        slap = GetComponent<AudioSource>();
        tisrigid = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        audiocd -= 1 * Time.deltaTime;
        if(tisrigid.velocity.sqrMagnitude > 1.4f)
        {
            if(audiocd < 0)
            {walkingdownthestreet.Play(0); audiocd = 1;}
        }
        
    }
    void OnTriggerEnter(Collider other) 
    {   
     
        GameObject toher = other.gameObject;
        if(toher.tag == "Player")
        {
            slap.Play(0);
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
