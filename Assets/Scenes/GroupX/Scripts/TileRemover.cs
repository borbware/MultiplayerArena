using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRemover : MonoBehaviour
{
    float interval, reset;

    List<GameObject> tiles;


    // Start is called before the first frame update
    void Start()
    {
        reset = 1.0f;

        tiles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        interval -= Time.deltaTime;

        if(interval < 0.0f)
        {
            var tileNum = Random.Range(0, 99);

            var tile = GameObject.Find("Cube (" + tileNum + ")");

            if(tile != null && StageManager.instance.stageState == StageManager.StageState.Play)
            {
                // tile.SetActive(false);

                List<Material> mat = new List<Material>();
                tile.GetComponent<MeshRenderer>().GetMaterials(mat);
                mat[0].SetColor("_Color", Color.red);

                tiles.Add(tile);

                Invoke("DestroyTile", 3.0f);
            }

            interval = reset;
        }
    }

    void DestroyTile()
    {
        tiles[0].SetActive(false);

        tiles.Remove(tiles[0]);
    }
}
