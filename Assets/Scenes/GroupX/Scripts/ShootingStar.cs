using UnityEngine;

public class ShootingStar : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction = new Vector3(-1f, -1f, 0f);
    public float delay = 3f;

    private float timer = 0f;
    private bool started = false;

    void Update()
    {
        if (!started)
        {
            timer += Time.deltaTime;

            if (timer >= delay)
            {
                started = true;
            }
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);

            if (transform.position.x < -20f || transform.position.y < -20f)
            {
                Destroy(gameObject);
            }
        }
    }
}
