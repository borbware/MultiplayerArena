using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float duration = 0.5f; // The duration of the screen shake
    public float magnitude = 0.1f; // The magnitude of the screen shake

    private Vector3 originalPosition; // The original position of the camera
    private float elapsed = 99999f; // The amount of time that has elapsed since the screen shake started

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = originalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
        }
        else
        {
            transform.position = originalPosition;
        }
    }

    public void Shake()
    {
        originalPosition = transform.position;
        elapsed = 0f;
    }
}
