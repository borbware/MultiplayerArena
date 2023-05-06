using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GroupX
{
    public class MoleSpawner : MonoBehaviour
    {
        [SerializeField] float spawnInterval = 2;
        [SerializeField] int maxMoles = 7; //this should be less than the no of players 
                                           //to create tension (not true - more testing needed)
        [SerializeField] GameObject mole;
        [SerializeField] GameObject hole;

        System.Random rand = new System.Random();

        //we construct our array of mole holes
        //new or different holes should be entered here manually
        /*static MoleHole hole_0 = new MoleHole(2.5f, 0f, 2.5f);
        static MoleHole hole_1 = new MoleHole(2.5f, 0f, -2.5f);
        static MoleHole hole_2 = new MoleHole(-2.5f, 0f, -2.5f);
        static MoleHole hole_3 = new MoleHole(-2.5f, 0f, 2.5f);*/

        [SerializeField] public MoleHole[] arrayOfHoles; // = {hole_0, hole_1, hole_2, hole_3};
        [SerializeField] AudioSource virusHitAudio;

        public List<MoleHole> listOfHoles = new List<MoleHole>();

        private void spawnMole()
        {
            /*picks a random index from the array of holes repeatedly until it finds
            an empty hole, then it spawns a new mole at that position
            the new mole remembers the index of the hole where it spawned*/

            int number_of_moles = GameObject.FindGameObjectsWithTag("Mole").Length;
            if (number_of_moles < maxMoles)
            {
                int holeNumber = rand.Next(0, listOfHoles.Count);
                while (!listOfHoles[holeNumber].isEmpty)
                {
                    holeNumber = rand.Next(0, listOfHoles.Count);
                }

                GameObject newMole = Instantiate<GameObject>(
                    mole,
                    listOfHoles[holeNumber].position + new Vector3(0f, -0.1f, 0f),
                    Quaternion.Euler(0f, 0f, 0f)
                );
                newMole.GetComponent<MoleScript>().iAmInHoleNo = holeNumber;
                Destroy(newMole, newMole.GetComponent<MoleScript>().moleLifetime);
                listOfHoles[holeNumber].isEmpty = false;
                //Debug.Log($"hole no {holeNumber} is full");
            }
        }

        public void playVirusHitAudio()
        {
            virusHitAudio.Play();
        }

        void Awake()
        {
            /*//we instantiate all the holes where the viruses will spawn
            foreach (MoleHole molehole in arrayOfHoles)
            {
                Instantiate<GameObject>(
                    hole,
                    molehole.position + new Vector3(0f, 0.1f, 0f),  //they are raised above the plain
                    Quaternion.Euler(0f, 0f, 0f)
                );
            }*/

        }

        // Start is called before the first frame update
        void Start()
        {
            listOfHoles.AddRange(arrayOfHoles);

            // start spawning moles after 3 seconds
            InvokeRepeating("spawnMole", 3f, spawnInterval);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}