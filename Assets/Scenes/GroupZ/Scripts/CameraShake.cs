using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    Transform transformShake;
    float shakeDuration;
    float shakeMagnitude = 0.2f;
    float dampingSpeed = 1.0f;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        if (transformShake == null)
        {
            transformShake = gameObject.GetComponent<Transform>();
        }
    }

    void OnEnable() 
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transformShake.localPosition = initialPosition + 
            Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        } else {
            shakeDuration = 0f;
            transformShake.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float shakeCamera) 
    {
        shakeDuration = shakeCamera;
    }
}
