using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupY
{
    public class ProceduralGrid : MonoBehaviour
    {
        [SerializeField]GameObject tile;
        public int width, depth;
        public float spacing = 1;
        int tileCount;

        public GameObject[] tiles;
        // Start is called before the first frame update
        void Start()
        {
         Generate();   
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Generate()
        {
            if(tile)
            {
                tileCount = width * depth;
                tiles = new GameObject[width * depth];

                //center the generated grid
                Vector3 offset = new Vector3(((float)width/2)* spacing - spacing * 0.5f,0,((float)depth/2)* spacing - spacing * 0.5f);

                for(int i = 0; i < width; i++)
                {
                    for(int j = 0; j < depth; j++)
                    {
                        tiles[i*width + j] = GameObject.Instantiate(tile, new Vector3((float)i * spacing,0,(float)j * spacing) - offset, Quaternion.Euler(0,0,0),this.transform);
                    }
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            if(tile)
            {


                
               Gizmos.DrawWireCube(transform.position, new Vector3((float)width * spacing, 0, (float)depth * spacing));
            }
        }


    }
}