using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GroupZ
{

public class MusicPlayer : MonoBehaviour
{
    AudioSource music;
    
    public AudioClip[] songList = new AudioClip[5];
   
    public float volume = 0.2f;

    public int songIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        songIndex  = Random.Range(0,5);
    }

    // Update is called once per frame
    void Update()
    {
      if (StageManager.instance.stageState == StageManager.StageState.Play)
      {
        if(!music.isPlaying)
        {
            music.PlayOneShot(songList[songIndex], volume);
        }
      }  
    }
}
}