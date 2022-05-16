using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_falling_script : MonoBehaviour
{
    GameObject[] platforms;
    bool hasbeendropped = false;

    float LastSpawn = 0f;

    int Dropped;



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
        yield return new WaitForSeconds (1.0f);
        yield return DropPlatform(platforms[5]);
    }


    void Update()
    {
    //    if(Time.time >2.0f && hasbeendropped == false) {
    //        StartCoroutine(StartDrop()); 
    //        hasbeendropped = true;
    //    }

        if(StageManager.instance.stageTime < 76 && (Time.time - 5) > LastSpawn){
            LastSpawn = Time.time;
            Dropped = Random.Range(0,15);
            int Dropped2 = Random.Range(0,15);

           if(Dropped == Dropped2) Dropped2 ++;
                StartCoroutine(PlatformDrop(Dropped));
                StartCoroutine(PlatformDrop(Dropped2));
        }
    }

    IEnumerator PlatformDrop(int dropped)
    {
        if(dropped >  15) dropped -= 15;
        yield return MovePlatform(platforms[dropped], -10f, 1.2f);
        yield return MovePlatform(platforms[dropped], 10f, 1f);
    }
    
    IEnumerator MovePlatform(GameObject platform, float value, float speed)
    {
        if(value < 0){
             for (int j = 0; j < 3; j++){
                 StartCoroutine(ColorFlash(platform));
                 if (j < 2)
                     yield return new WaitForSeconds (0.45f);
                 else
                     yield return new WaitForSeconds (0.15f);
             }
        }
        Debug.Log(platform.name);
        yield return LerpFunction.LerpPosition(platform.transform, 
        platform.transform.position + new Vector3(0f, value, 0f), speed);
    }

    IEnumerator ColorFlash(GameObject Object)
    {
        Renderer PlatRenderer = Object.GetComponent<Renderer>();
        yield return PlatRenderer.material.color = Color.red;
        yield return new WaitForSeconds (0.15f);
        yield return PlatRenderer.material.color = Color.white;
    }
}
