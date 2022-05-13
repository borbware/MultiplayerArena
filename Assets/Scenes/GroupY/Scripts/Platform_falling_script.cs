using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_falling_script : MonoBehaviour
{
    GameObject[] platforms;
    bool hasbeendropped = false;



    void Start()
    {
       platforms = GameObject.FindGameObjectsWithTag("Platform");

    }

    IEnumerator DropPlatform(GameObject platform)
    {
        platform.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(2.0f);
        yield return LerpFunction.LerpPosition(platform.transform, 
        platform.transform.position + new Vector3(0f, -10f, 0f), 2.0f);
    }    
    
    IEnumerator StartDrop()
    {
        yield return DropPlatform(platforms[0]);
        yield return new WaitForSeconds (5.0f);
        yield return DropPlatform(platforms[5]);
    }


    void Update()
    {
       if(Time.time >2.0f && hasbeendropped == false) {
           StartCoroutine(StartDrop()); 
           hasbeendropped = true;
       }
    }
}
