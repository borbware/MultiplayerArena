using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDropEvent : MonoBehaviour
{
    float timeTillDrop = 5f;
    float totalStageTime;
    bool eventTriggered = false;
    bool warningSounded = false;
    
    [SerializeField] AudioSource dropWarningAudio;
    [SerializeField] AudioSource floorDropAudio;
    [SerializeField] List<GameObject> outerHexes;

    StageManager stageManager;
    List<MeshRenderer> meshRenderers = new();

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

    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        totalStageTime = stageManager.stageTime;

        foreach (GameObject hex in outerHexes)
            meshRenderers.AddRange(hex.GetComponentsInChildren<MeshRenderer>());
    }

    // Update is called once per frame
    void Update()
    {
        eventTrigger();
    }
}
