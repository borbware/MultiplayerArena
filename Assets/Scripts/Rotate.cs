using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField, Range(0.01f,   720f)] float rotationSpeed;
    
    void Update()
    {
		if (StageManager.instance.stageState != "play")
            return;
        transform.Rotate(new Vector3(0,rotationSpeed * Time.deltaTime,0));
    }
}
