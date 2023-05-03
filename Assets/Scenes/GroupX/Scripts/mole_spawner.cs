using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole_spawner : MonoBehaviour
{   
    [SerializeField] float spawn_interval = 3;
    [SerializeField] int max_moles = 3; //this should be less than the no of players 
                                        //to create tension
    [SerializeField] GameObject mole;

    System.Random rand = new System.Random();

    public class MoleHole {
        /* this clss is a container for a position (Vector3) and a bool which keeps track 
        whether there is a mole already spawned*/

        public Vector3 position;
        public bool is_empty = true;

        public MoleHole(float x, float y, float z){
            position = new Vector3(x, y, z);
        }

        public void set_empty(){
            is_empty = true;
        }
    }

    //we construct our array of mole holes
    //new or different holes should be entered here manually
    static MoleHole hole_0 = new MoleHole(2.5f, 0.5f, 2.5f);
    static MoleHole hole_1 = new MoleHole(2.5f, 0.5f, -2.5f);
    static MoleHole hole_2 = new MoleHole(-2.5f, 0.5f, -2.5f);
    static MoleHole hole_3 = new MoleHole(-2.5f, 0.5f, 2.5f);

    public MoleHole[] array_of_holes = {hole_0, hole_1, hole_2, hole_3};


    private void spawn_mole(){
        /*picks a random index from the array of holes repeatedly until it finds
        an empty hole, then it spawns a new mole at that position
        the new mole remembers the index of the hole where it spawned*/

        int number_of_moles = GameObject.FindGameObjectsWithTag("Mole").Length;
        if (number_of_moles < max_moles){
            int hole_number = rand.Next(0, array_of_holes.Length);
            while (!array_of_holes[hole_number].is_empty){
                hole_number = rand.Next(0, array_of_holes.Length);
            }
            
            GameObject new_mole = Instantiate<GameObject>(
                mole,
                array_of_holes[hole_number].position,
                Quaternion.Euler(0f, 0f, 0f)
            );
            new_mole.GetComponent<mole_script>().i_am_in_hole_no = hole_number;
            Destroy(new_mole, new_mole.GetComponent<mole_script>().mole_lifetime);
            //Debug.Log($"hole no {hole_number} is full");
        }
    }

    void Awake() {

    }

    // Start is called before the first frame update
    void Start(){
        // start spawning moles after 2 seconds
        InvokeRepeating("spawn_mole", 2f, spawn_interval);
    }

    // Update is called once per frame
    void Update(){
        
    }
}
