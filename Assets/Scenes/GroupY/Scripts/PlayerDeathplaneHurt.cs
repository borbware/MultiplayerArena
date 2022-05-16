using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathplaneHurt : MonoBehaviour
{

    public float threshold;

    [SerializeField] float damage = 50f;
    
    void Start() 
    {
        if (StageManager.instance.stageTime > 0f) {
            
        }
    }
    

    void FixedUpdate () {
        if (transform.position.y < threshold)
            transform.position = new Vector3(0f, 5f, 0f);
    }


    
}
