using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole_script : MonoBehaviour
{   
    [SerializeField] public float mole_lifetime = 5f;
    public int i_am_in_hole_no = 0;

    public enum moleState
    {
        isMovingUp,
        isWaiting,
        isMovingDown
    }

    private void OnDestroy() {
        // we set call the mole_spawner script to set the hole the mole was in to empty
        GameObject.Find("RunningScripts").GetComponent<mole_spawner>()
        .array_of_holes[i_am_in_hole_no].set_empty();
        //Debug.Log($"hole no {i_am_in_hole_no} is empty");
    }

    float moleWaitingTimer = 0f;
    float movementTimer = 0f;
    void moveMole(float distance, float waitingTime){
        /*moves the mole up the given distance, 
        waits waitingTime-1 seconds,
        then moves the mole down to its starting position*/

        Vector3 offset = new Vector3(0f, distance, 0f);
        moleState thisMoleState = moleState.isMovingUp;

        if (transform.position.y >= distance - 0.1){
            thisMoleState = moleState.isWaiting;
            movementTimer = 0;
            }
        if (moleWaitingTimer >= waitingTime - 1){thisMoleState = moleState.isMovingDown;}

        if (thisMoleState == moleState.isMovingUp){
            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + offset;
            transform.position = Vector3.Lerp(startPos, endPos, movementTimer/10);
            movementTimer += Time.deltaTime;
        }
        if (thisMoleState == moleState.isWaiting){
            moleWaitingTimer += Time.deltaTime;
        }
        if (thisMoleState == moleState.isMovingDown){
            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position - offset;
            transform.position = Vector3.Lerp(startPos, endPos, movementTimer/2);
            movementTimer += Time.deltaTime;
        }

        
        
        
    }


    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        moveMole(0.6f, mole_lifetime - 1);
    }
}
