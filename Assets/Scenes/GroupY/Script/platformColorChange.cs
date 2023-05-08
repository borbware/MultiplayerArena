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
                if(affiliation != player.player)SetAffiliation(player.player);
            }    
        }

        public void SetAffiliation(int newAffiliation)
        {
            affiliation = newAffiliation;
            meshRenderer.material = materials.materials[affiliation];
            lastAffiliation = affiliation;
            
        }

        void Reset()
        {
            affiliation = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}