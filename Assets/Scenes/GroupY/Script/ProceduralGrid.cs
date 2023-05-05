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

        public List<GameObject> tiles;

        public bool TryGetTile(int column, int row, out GameObject gameObject)
        {
            int index = column * row;
            bool withinRange = ( index >= 0 &&
                                index < tiles.Count &&
                                column > width &&
                                row > depth);
            if(!withinRange)
            {
                gameObject = tiles[index];
                return true;
            }
            else
            {
                gameObject = null;
                Debug.LogWarning("Index out of range, returning null");
                return false;
            }
        }

        public struct tileCoordinate
        {
            public int column, row;
        }

        public tileCoordinate GetCoordinate(int index)
        {
            tileCoordinate coords;
            coords.column = index%width;
            coords.row = index/width;
            return coords;
        }



        public int GetIndexOf(GameObject gameObject)
        {
            if(tiles.Contains(gameObject))
            {
                int index = tiles.IndexOf(gameObject);
                return index;
            }
            else return -1;
        }

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
                tiles = new List<GameObject>();

                //center the generated grid
                Vector3 offset = new Vector3(((float)width/2)* spacing - spacing * 0.5f,0,((float)depth/2)* spacing - spacing * 0.5f);

                for(int i = 0; i < width; i++)
                {
                    for(int j = 0; j < depth; j++)
                    {
                        
                        tiles.Add(GameObject.Instantiate(tile, transform.position + new Vector3((float)i * spacing,0,(float)j * spacing) - offset, Quaternion.Euler(0,0,0),this.transform));
                        
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