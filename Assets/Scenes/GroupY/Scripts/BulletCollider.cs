using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    Rigidbody rb;
    Rigidbody destRB;

    Vector3 KnockbackDir;
    void Start(){
        rb = gameObject.GetComponent<Rigidbody>();
        KnockbackDir = rb.transform.forward;
        //newBullet.GetComponent<Rigidbody>().AddForce(
                    //transform.forward * shootForce * Time.fixedDeltaTime);
    }
    [SerializeField] float damage = 10f;
    void OnCollisionEnter(Collision C){
        Debug.Log(C.gameObject.tag);
        C.gameObject.SendMessage("Hurt", damage, SendMessageOptions.DontRequireReceiver);
        if(C.gameObject.tag == "Player")
        C.gameObject.GetComponent<Rigidbody>().AddForce(
        KnockbackDir * 5);
            Destroy(gameObject);
    }
}
