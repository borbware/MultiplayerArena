using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRemover : MonoBehaviour
{
    float interval, reset;


    // Start is called before the first frame update
    void Start()
    {
        reset = 1.0f;
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
                tile.SetActive(false);

            interval = reset;
        }
    }
}
