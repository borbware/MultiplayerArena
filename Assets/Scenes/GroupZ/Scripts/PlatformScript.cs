using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupZ
{
    public class PlatformScript : MonoBehaviour
    {
        MeshCollider platformMesh;
        GameObject meshChild;
        float platformHP = 5f;
        //float lerpMax = 0f;
        //float lerpMin = -10f;
        //float fallLerptime;
        //float lerpTemp;
        bool playerTouching = false;
        bool platformFallen = false;
        Vector3 startPos;
        Vector3 childstartPos;
        // Start is called before the first frame update

        void Shaking(float speed, float amount)
        {
            meshChild.transform.localPosition = new Vector3
                                            (childstartPos.x + Mathf.Sin(Time.time * speed) * amount, 
                                            childstartPos.y + Mathf.Sin(Time.time * speed) * amount,
                                            transform.localPosition.z);
        }

        void Falling()
        {
            transform.position = new Vector3(transform.position.x, -10, transform.position.z);
            platformFallen = true;
        }

        void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.tag == "Bullet")
            {
                platformHP -= 1.5f;
                CameraShake.instance.TriggerShake(0.1f);
            }
        }

        void OnTriggerExit(Collider other) 
        {
            if (other.gameObject.tag == "Bullet")
            {
                meshChild.transform.localPosition = childstartPos;
            }
        }

        void OnCollisionStay(Collision other) 
        {
            if (other.gameObject.tag == "Player")
            {
                playerTouching = true;
            }
        }

        void OnCollisionExit(Collision other) 
        {
            if (other.gameObject.tag == "Player")
            {
                playerTouching = false;
                meshChild.transform.localPosition = childstartPos;
            }
        }

        void Start()
        {
            meshChild = gameObject.transform.GetChild(0).gameObject;
            platformMesh = meshChild.GetComponent<MeshCollider>();
            startPos = new Vector3
                            (transform.position.x,
                            transform.position.y,
                            transform.position.z);
            childstartPos = new Vector3
                            (meshChild.transform.localPosition.x, 
                            meshChild.transform.localPosition.y, 
                            meshChild.transform.localPosition.z);
        }

        // Update is called once per frame
        void Update()
        {
            if (StageManager.instance.stageState == StageManager.StageState.Play)
            {
                if (playerTouching == true)
                {
                    Shaking(20, 0.1f);
                    platformHP -= Time.deltaTime;
                }

                if (platformFallen == true)
                {
                    platformHP += Time.deltaTime;
                }

                if (platformHP <= 0)
                {
                    //fallLerptime += Time.deltaTime;
                    Falling();
                } else if (platformHP >= 5f && platformFallen == true)
                {
                    platformHP = 5f;
                    transform.position = new Vector3(startPos.x, startPos.y, startPos.z);
                    //lerpTemp = lerpMax;
                    //lerpMax = lerpMin;
                    //lerpMin = lerpTemp;
                    platformFallen = false;
                }
            }
        }
    }
}

