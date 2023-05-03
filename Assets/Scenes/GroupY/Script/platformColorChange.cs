using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupY
{
    public class platformColorChange : MonoBehaviour
    {
        [SerializeField] GameObject players;

        [Range(0, 4)]
        public int affiliation;
        int lastAffiliation;

        [SerializeField] teamMaterials materials;
        [SerializeField] MeshRenderer meshRenderer;

        void Start()
        {
            meshRenderer.material = materials.materials[affiliation];
        }
        void OnCollisionEnter(Collision other) 
        {
        if (other.gameObject.tag == "Player")
            {   Player player = other.collider.GetComponent<Player>();
                if(affiliation != player.player)OnCapture(player.player);
            }    
        }

        void OnCapture(int newAffiliation)
        {
            affiliation = newAffiliation;
            if(affiliation != 0)StageManager.instance.UIManagers[affiliation - 1].AddScore(1);
            if(lastAffiliation != 0)StageManager.instance.UIManagers[lastAffiliation - 1].AddScore(-1);
            meshRenderer.material = materials.materials[affiliation];
            lastAffiliation = affiliation;
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}