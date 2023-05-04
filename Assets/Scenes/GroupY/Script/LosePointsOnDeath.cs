using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePointsOnDeath : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    void OnDestroy()
    {
        player.UIManager.AddScore(-player.UIManager.score);
    }
}
