using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class hazardishot : MonoBehaviour
{
    int shotPower;
    CollisionModule col;
    GameObject tuli;
    
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponentInChildren<ParticleSystem>().collision;
        tuli = GameObject.Find("HazardiFlame");
    }

    // Update is called once per frame
    void Update()
    {
        shotPower = tuli.transform.GetComponentInParent<hazardi>().shotPower;
        col.colliderForce = shotPower;
    }
}
