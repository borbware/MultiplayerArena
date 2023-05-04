using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupY
{
    public class BankerManager : MonoBehaviour
    {
        public static BankerManager instance;

        public List<ProceduralGrid> grids;
        // Start is called before the first frame update
        void Start()
        {
            if(instance != null) Destroy(this.gameObject);
            else instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Bank(int player)
        {
            int total = 0;
            foreach(ProceduralGrid grid in grids)
            {
                platformColorChange[] tiles = grid.GetComponentsInChildren<platformColorChange>();
                foreach(platformColorChange tile in tiles)
                {
                    if(tile.affiliation == player)
                    {
                        total += 1;
                        tile.SetAffiliation(0);
                    }
                }

            }
            StageManager.instance.UIManagers[player - 1].AddScore(total);
        }
    }
}