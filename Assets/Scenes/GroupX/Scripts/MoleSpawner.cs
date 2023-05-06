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

        [SerializeField] public MoleHole[] arrayOfHoles;
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
            }
        }

        public void playVirusHitAudio()
        {
            virusHitAudio.Play();
        }

        // Start is called before the first frame update
        void Start()
        {
            listOfHoles.AddRange(arrayOfHoles);

            // start spawning moles after 3 seconds
            InvokeRepeating("spawnMole", 3f, spawnInterval);
        }
    }
}