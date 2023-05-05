using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDropEvent : MonoBehaviour
{
    double timeTillDrop;
    double totalStageTime;
    float dropAfterPercent = 0.6f;  //the stage drop after this amount of the total stage time has elapsed
    bool eventTriggered = false;
    bool warningSounded = false;
    
    [SerializeField] AudioSource dropWarningAudio;
    [SerializeField] AudioSource floorDropAudio;
    [SerializeField] List<GameObject> outerHexes;

    StageManager stageManager;
    List<MeshRenderer> meshRenderers = new();

    //feel free to change these 4 vectors and experiment with them
    Vector3 mainCameraFarPosition = new Vector3(-339.5f, -200f, 41f);
    Quaternion mainCameraFarRotation = Quaternion.Euler(66.5f, 0f, 0f);
    Vector3 mainCameraClosePosition = new Vector3(-340f, -207f, 43f);
    Quaternion mainCameraCloseRotation = Quaternion.Euler(45f, 0f, 0f);

    GameObject mainCamera;
    float lerpTimePassed = 0f;
    float lerpDuration = 3f;


    void eventTrigger(){
        float currentStageTime = stageManager.stageTime;

        //warning sounds & colors
        if (totalStageTime - currentStageTime >= timeTillDrop - 2.9 && !warningSounded){
            warningSounded = true;
            InvokeRepeating("playWarningSound", 0.1f, 1f);
            foreach (var meshRenderer in meshRenderers)
                StartCoroutine(FlashReddish(meshRenderer));
        }

        //the event
        if (totalStageTime - currentStageTime > timeTillDrop && !eventTriggered){  
            eventTriggered = true;

            //remove the extra moleholes - removes 6 elements starting from index 7 (these are the last 6 holes)
            GetComponent<MoleSpawner>().listOfHoles.RemoveRange(7, 6);

            //drop outer hexes
            floorDropAudio.Play();
            foreach (GameObject hex in outerHexes)
            {
                hex.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;  
                hex.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    void playWarningSound(){
        if (!eventTriggered){dropWarningAudio.Play();}   
    }

    IEnumerator FlashReddish(MeshRenderer meshRenderer)
    {
        float startTime = Time.time;
        Material material = meshRenderer.material;
        Color originalColor = material.color;

        Color reddish = Color.Lerp(originalColor, Color.red, 0.3f);

        while (!eventTriggered)
        {
            float delta = (Time.time - startTime) % 1f;
            float deltaDiffFromHalf = Mathf.Abs(0.5f - delta);
            float howFarFromReddish = Mathf.InverseLerp(0, 0.5f, deltaDiffFromHalf);
            material.color = Color.Lerp(reddish, originalColor, howFarFromReddish);
            yield return null;
        }

        material.color = originalColor;
    }

    private void moveCamera(){
        if (eventTriggered && mainCamera.transform.position != mainCameraClosePosition){
            if (lerpTimePassed < lerpDuration){
                mainCamera.transform.localPosition =
                    Vector3.Lerp(mainCameraFarPosition, mainCameraClosePosition, lerpTimePassed/lerpDuration);

                mainCamera.transform.localRotation = 
                    Quaternion.Lerp(mainCameraFarRotation, mainCameraCloseRotation, lerpTimePassed/lerpDuration);

                lerpTimePassed += Time.deltaTime;
            }
            else{
                mainCamera.transform.localPosition = mainCameraClosePosition;
                mainCamera.transform.localRotation = mainCameraCloseRotation;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        totalStageTime = stageManager.stageTime;
        timeTillDrop = totalStageTime * dropAfterPercent;

        foreach (GameObject hex in outerHexes)
            meshRenderers.AddRange(hex.GetComponentsInChildren<MeshRenderer>());
        
        mainCamera = GameObject.Find("Main Camera");
    }

    private void getCameraPosition(){   //for debug purposes only
        if (Input.GetKeyDown(KeyCode.C)){
            Debug.Log(mainCamera.transform.position);
            Debug.Log(mainCamera.transform.rotation.eulerAngles);
        }
    }

    // Update is called once per frame
    void Update()
    {
        eventTrigger();
        moveCamera();
        getCameraPosition();
    }
}
