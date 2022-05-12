using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokkaus : MonoBehaviour
{
    float pushforce = 100;
    Player player;
    Vector3 spawnlocation;
    AudioSource walking;
    [SerializeField] AudioClip slap;
    Rigidbody tisrigid;
    float maxVel = 12f;
    float liftUp = 25f;
    [SerializeField] float audiocd = 0, dashspeed = 5;
    bool canDash = true;

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
        if(player.GetComponent<Player>().shootInput && canDash == true)
        {
            StartCoroutine(DashCooldown());
        }
        else if (player.GetComponent<Player>().shootInput && canDash == false)
        {
            Debug.Log("Can't dash");
        }

        if(tisrigid.velocity.magnitude > maxVel)
            tisrigid.velocity = tisrigid.velocity.normalized * maxVel; 
    }
    IEnumerator DashCooldown()
    {

        canDash = true;
        yield return new WaitForSeconds(0.01f);
        tisrigid.AddForce(transform.up * liftUp);
        tisrigid.AddForce(transform.forward * dashspeed);
        canDash = false;
        if(tisrigid.velocity.magnitude > maxVel)
            tisrigid.velocity = tisrigid.velocity.normalized * maxVel;
        yield return new WaitForSeconds(2f);
        canDash = true;
    }
    void OnCollisionEnter(Collision other) 
    {   
     
        GameObject toher = other.gameObject;
        if(toher.tag == "Player")
        {
            // Debug.Log("triggering");
            walking.PlayOneShot(slap);
            var pushother = toher.GetComponent<Rigidbody>();
            var dist = toher.transform.position - gameObject.transform.position;
            var dotproduct = Vector3.Dot(tisrigid.velocity, dist)/dist.magnitude;
            if (dotproduct > 0)
                pushother.AddForce(dist.normalized * dotproduct * pushforce);
        }    
        if(toher.tag == "fial")
        {
            // Debug.Log(toher.tag);
            if(player.UIManager.score == 0)
            {Destroy(gameObject);}
            else
            {player.UIManager.AddScore(-1); transform.position = spawnlocation;}
        }
    }
}
