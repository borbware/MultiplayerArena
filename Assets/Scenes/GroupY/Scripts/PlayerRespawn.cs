using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    GameObject[] platforms;

    GameObject ClosestGameObject;

    void Start()
    {
        platforms = GameObject.FindGameObjectsWithTag("Platform");
    }

    void Update()
    {
        if(transform.position.y < -0.5f){
            gameObject.SendMessage("Hurt", 30f, SendMessageOptions.DontRequireReceiver);
            transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
            ClosestGameObject = ClosestObject();

            if(ClosestGameObject != null){
                transform.position = 
                    new Vector3(ClosestGameObject.transform.position.x, 
                    transform.position.y, ClosestGameObject.transform.position.z);
            }
        }
    }

    GameObject ClosestObject(){
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in platforms)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
