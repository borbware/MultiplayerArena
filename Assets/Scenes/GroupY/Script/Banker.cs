using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupY
{
    [RequireComponent(typeof(BoxCollider))]
    public class Banker : MonoBehaviour
    {

        public List<Transform> positions;
        int pos;

        BoxCollider coll;
        // Start is called before the first frame update
        void Start()
        {
            coll = GetComponent<BoxCollider>();
            transform.position = positions[0].position;
        }

        // Update is called once per frame
        void Update()
        {

        }
    
        void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.tag == "Player")
            {
                Player player = other.gameObject.GetComponent<Player>();
                BankerManager.instance.Bank(player.player);

                pos++;
                StartCoroutine(Teleport(0.25f, positions[pos%positions.Count].position));
            }
        }

        IEnumerator Teleport(float duration, Vector3 position)
        {   
            coll.enabled = false;
            for(float elapsed = 0; elapsed <= duration; elapsed += Time.deltaTime)
            {
                transform.rotation = Quaternion.Euler(0,elapsed/duration*90,0);
                transform.localScale = Vector3.one * (1 - (elapsed/duration));
                yield return null;
            }
            transform.position = position;
            for(float elapsed = 0; elapsed <= duration; elapsed += Time.deltaTime)
            {
                transform.rotation = Quaternion.Euler(0,elapsed/duration*90,0);
                transform.localScale = Vector3.one * (elapsed/duration);
                yield return null;
            }
            coll.enabled = true;
        }
    }
}