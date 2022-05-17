using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tokkaus : MonoBehaviour
{
    float pushforce = 130;
    Player player;
    Vector3 spawnlocation;
    AudioSource walking, othersource;
    [SerializeField] AudioClip slap, death;
    Rigidbody tisrigid;
    float maxVel = 17f;
    float liftUp = 25f;
    [SerializeField] float audiocd = 0;
    float  dashspeed = 400; 
    bool canDash = true;

    void Start()
    {
        spawnlocation = transform.position;
        player = GetComponent<Player>();
        walking = GetComponent<AudioSource>();
        othersource = GameObject.Find("Environment").GetComponent<AudioSource>();
        tisrigid = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        // Debug.Log(pushforce);
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
    IEnumerator Spawning() {
        player.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        player.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        player.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
        player.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = false;
        player.transform.GetChild(4).GetComponent<MeshRenderer>().enabled = false;
        player.transform.GetChild(5).GetComponent<MeshRenderer>().enabled = false;
        player.transform.GetChild(6).GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        player.UIManager.AddScore(-1); 
        transform.position = spawnlocation;
        player.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        player.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
        player.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
        player.transform.GetChild(3).GetComponent<MeshRenderer>().enabled = true;
        player.transform.GetChild(4).GetComponent<MeshRenderer>().enabled = true;
        player.transform.GetChild(5).GetComponent<MeshRenderer>().enabled = true;
        player.transform.GetChild(6).GetComponent<MeshRenderer>().enabled = true;
    }

    IEnumerator DashCooldown()
    {
        canDash = true;
        yield return new WaitForSeconds(0.01f);
        tisrigid.AddForce(transform.up * liftUp);
        for (int i = 0; i < 15; i++)
        {
            pushforce = 300;
            tisrigid.AddForce(transform.forward * dashspeed);
            yield return new WaitForSeconds(0.01f);
        }
        canDash = false;
        pushforce = 130;
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
        if(toher.tag == "Failure")
        {
            othersource.PlayOneShot(death);
            if(player.UIManager.score == 0) {
                Destroy(gameObject);
            }
            else {
                StartCoroutine(Spawning());
            }
        }
    }
}
