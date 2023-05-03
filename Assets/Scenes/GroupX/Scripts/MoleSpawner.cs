using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : MonoBehaviour
{   
    [SerializeField] float spawnInterval = 3;
    [SerializeField] int maxMoles = 3; //this should be less than the no of players 
                                        //to create tension
    [SerializeField] GameObject mole;
    [SerializeField] GameObject hole;

    System.Random rand = new System.Random();

    public class MoleHole {
        /* this clss is a container for a position (Vector3) and a bool which keeps track 
        whether there is a mole already spawned*/

        public Vector3 position;
        public bool isEmpty = true;

        public MoleHole(float x, float y, float z){
            position = new Vector3(x, y, z);
        }

        public void setEmpty(){
            isEmpty = true;
        }
    }

    //we construct our array of mole holes
    //new or different holes should be entered here manually
    static MoleHole hole_0 = new MoleHole(2.5f, 0f, 2.5f);
    static MoleHole hole_1 = new MoleHole(2.5f, 0f, -2.5f);
    static MoleHole hole_2 = new MoleHole(-2.5f, 0f, -2.5f);
    static MoleHole hole_3 = new MoleHole(-2.5f, 0f, 2.5f);

    public MoleHole[] arrayOfHoles = {hole_0, hole_1, hole_2, hole_3};


    private void spawnMole(){
        /*picks a random index from the array of holes repeatedly until it finds
        an empty hole, then it spawns a new mole at that position
        the new mole remembers the index of the hole where it spawned*/

        int number_of_moles = GameObject.FindGameObjectsWithTag("Mole").Length;
        if (number_of_moles < maxMoles){
            int holeNumber = rand.Next(0, arrayOfHoles.Length);
            while (!arrayOfHoles[holeNumber].isEmpty){
                holeNumber = rand.Next(0, arrayOfHoles.Length);
            }
            
            GameObject newMole = Instantiate<GameObject>(
                mole,
                arrayOfHoles[holeNumber].position + new Vector3(0f, -0.1f, 0f),
                Quaternion.Euler(0f, 0f, 0f)
            );
            newMole.GetComponent<MoleScript>().iAmInHoleNo = holeNumber;
            Destroy(newMole, newMole.GetComponent<MoleScript>().moleLifetime);
            arrayOfHoles[holeNumber].isEmpty = false;
            //Debug.Log($"hole no {holeNumber} is full");
        }
    }

    void Awake() {
        //we instantiate all the holes where the viruses will spawn
        foreach (MoleHole molehole in arrayOfHoles)
        {
            Instantiate<GameObject>(
                hole,
                molehole.position + new Vector3(0f, 0.1f, 0f),  //they are raised above the plain
                Quaternion.Euler(0f, 0f, 0f)
            );
        }

    }

    // Start is called before the first frame update
    void Start(){
        // start spawning moles after 2 seconds
        InvokeRepeating("spawnMole", 3f, spawnInterval);
    }

    // Update is called once per frame
    void Update(){
        
    }
}
