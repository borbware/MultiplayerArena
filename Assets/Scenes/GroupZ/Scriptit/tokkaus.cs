using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokkaus : MonoBehaviour
{
    float pushforce = 7;
    Player player;
    Vector3 spawnlocation;
    [SerializeField] AudioClip slap;
    void Start()
    {
        spawnlocation = transform.position;
        player = GetComponent<Player>();
    }

    
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) 
    {   
        var tisrigid = gameObject.GetComponent<Rigidbody>();
        GameObject toher = other.gameObject;
        if(toher.tag == "Player")
        {
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
