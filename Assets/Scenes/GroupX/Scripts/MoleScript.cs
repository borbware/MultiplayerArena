using System.Collections;
using System.Collections.Generic;

using GroupX;

using UnityEngine;

public class MoleScript : MonoBehaviour, IHittableByPlayer
{   
    [SerializeField] public float moleLifetime = 3f;
    public int iAmInHoleNo = 0;
    GameObject runningScripts;
    private ParticleSystem moleParticles;

    private bool _dying = false;

    // public enum moleState
    // {
    //     isMovingUp,
    //     isWaiting,
    //     isMovingDown
    // }

    private void OnDestroy() {
        // we set call the MoleSpawner script to set the hole the mole was in to empty
        if (iAmInHoleNo < runningScripts.GetComponent<MoleSpawner>().listOfHoles.Count - 1){
            runningScripts.GetComponent<MoleSpawner>().listOfHoles[iAmInHoleNo].setEmpty();
            //Debug.Log($"hole no {iAmInHoleNo} is empty");
        }
    }

    public void GetHitBy(PlayerController player)
    {
        if (!_dying)
        {
            _dying = true;

            runningScripts.GetComponent<MoleSpawner>().playVirusHitAudio();
            GetComponent<MeshRenderer>().enabled = false;
            moleParticles.Play();
            player.GetComponent<Player>().UIManager.AddScore(1);

            Invoke("DestroyThisVirus", 5f);
        }
    }
    void DestroyThisVirus() {
        Destroy(gameObject);
    }

    // float moleWaitingTimer = 0f;
    // float movementTimer = 0f;

    //the function below uses lerp and the enum states; it's simpler without it
    
    // void moveMole(float distance, float waitingTime){
    //     /*moves the mole up the given distance, 
    //     waits waitingTime-1 seconds,
    //     then moves the mole down to its starting position*/

    //     Vector3 offset = new Vector3(0f, distance, 0f);
    //     moleState thisMoleState = moleState.isMovingUp;
        
        
    //     if (transform.position.y >= distance - 0.1){
    //         thisMoleState = moleState.isWaiting;
    //         movementTimer = 0;
    //         }
    //     if (moleWaitingTimer >= waitingTime - 1){thisMoleState = moleState.isMovingDown;}

    //     if (thisMoleState == moleState.isMovingUp){
    //         Vector3 startPos = transform.position;
    //         Vector3 endPos = transform.position + offset;
    //         transform.position = Vector3.Lerp(startPos, endPos, movementTimer/12);
    //         movementTimer += Time.deltaTime;
    //     }
    //     if (thisMoleState == moleState.isWaiting){
    //         moleWaitingTimer += Time.deltaTime;
    //     }
    //     if (thisMoleState == moleState.isMovingDown){
    //         Vector3 startPos = transform.position;
    //         Vector3 endPos = transform.position - offset;
    //         transform.position = Vector3.Lerp(startPos, endPos, movementTimer/12);
    //         movementTimer += Time.deltaTime;
    //     }  
    // }
    

    void moveUp(){
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 1.2f, 0f);
    }

    void moveDown(){
        GetComponent<Rigidbody>().velocity = new Vector3(0f, -1.2f, 0f);
    }

    bool stopped = false;
    void checkStopped(){
        if (!stopped && transform.position.y >= 0.5){
            stopped = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            Invoke("moveDown", moleLifetime-1);
        }
    }

    // Start is called before the first frame update
    void Start(){
        runningScripts = GameObject.Find("RunningScripts");
        moleParticles = GetComponent<ParticleSystem>();
        moveUp();
    }

    // Update is called once per frame
    void Update(){
        //moveMole(0.6f, moleLifetime - 1);
        checkStopped();
    }
}
