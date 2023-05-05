using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupY
{
    public class RespawnOnFall : MonoBehaviour
    {
        [SerializeField] int pointsPunishment;
        Vector3 StartPosition;
        [SerializeField] string fallBarrierName = "FallBarrier";
        Player player;
        Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            StartPosition = transform.position;
            StartPosition.y += 0.1f;
            player = GetComponent<Player>();
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == fallBarrierName)
            {
                transform.position = StartPosition;
                rb.velocity = Vector3.zero;

                GridManager.instance.ClearGrids(player.player);
    
            if (player.UIManager.score - pointsPunishment < 0)
                    player.UIManager.AddScore(-player.UIManager.score);
                else
                    player.UIManager.AddScore(-pointsPunishment);

            }
        }
    }
}
