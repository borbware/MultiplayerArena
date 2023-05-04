using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupZ
{
    public class PlatformScript : MonoBehaviour
    {
        BoxCollider platformCollider;
        GameObject meshChild;
        MeshFilter meshf;
        [SerializeField] Mesh intact, broken1, broken2;
        [SerializeField] GameObject brokenPlatform;
        float platformHP = 5f;
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
                                            0);
        }

        void Falling()
        {
            GameObject platformBroke = Instantiate(brokenPlatform, transform.position, transform.rotation);
            Destroy(platformBroke, 2f);
            transform.position = new Vector3(transform.position.x, -10, transform.position.z);
            platformFallen = true;
            
        }

        void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.tag == "Bullet")
            {
                platformHP -= 1.7f;
                CameraShake.instance.TriggerShake(0.1f);
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
            platformCollider = meshChild.GetComponent<BoxCollider>();
            startPos = transform.position;
            childstartPos = meshChild.transform.localPosition;
            meshf = meshChild.GetComponent<MeshFilter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (StageManager.instance.stageState == StageManager.StageState.Play)
            {
                if (playerTouching == true)
                {
                    Shaking(20, 0.05f);
                    platformHP -= Time.deltaTime;
                }

                if (platformFallen == true)
                {
                    platformHP += Time.deltaTime;
                }

                if (platformHP <= 0)
                {
                    Falling();
                } else if (platformHP <= 1.666f)
                {
                    meshf.mesh = broken2;
                } else if (platformHP <= 3.444f)
                {
                    meshf.mesh = broken1;
                } else if (platformHP >= 5f && platformFallen == true)
                {
                    platformHP = 5f;
                    meshf.mesh = intact;
                    transform.position = startPos;
                    //lerpTemp = lerpMax;
                    //lerpMax = lerpMin;
                    //lerpMin = lerpTemp;
                    platformFallen = false;
                }
            }
        }
    }
}

