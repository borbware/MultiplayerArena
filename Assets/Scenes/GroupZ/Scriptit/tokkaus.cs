using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokkaus : MonoBehaviour
{
    float pushforce = 5;
    int hp = 2;
    Vector3 spawnlocation;
    void Start()
    {
        spawnlocation = transform.position;
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
            if(hp == 0)
            {Destroy(gameObject);}
            else
            {hp -= 1; transform.position = spawnlocation;}
            
        }
    }
}
