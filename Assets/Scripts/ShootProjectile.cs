using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    bool desiredShoot;
    float nextShootTime;
    [Range(0.01f,   10f)] public float shootPeriod;
    [Range(0f,   10000f)] public float shootForce;
    public GameObject bullet;

    Player _player;
    private void Start()
    {
        _player = GetComponent<Player>();
    }
    void Update()
    {
        if (StageManager.instance.stageState != StageManager.StageState.Play)
            return;
        if (_player != null)
        {
		    desiredShoot |= _player.shootInput;
        } else {
            desiredShoot = true;
        }
    }

    void FixedUpdate()
    {
        if (desiredShoot)
        {
            desiredShoot = false;
            if (bullet != null && Time.time >= nextShootTime)
            {
                var newBullet = Instantiate(
                    bullet,
                    transform.position + transform.forward * 0.5f,
                    Quaternion.identity
                );
                newBullet.GetComponent<Rigidbody>().AddForce(
                    transform.forward * shootForce * Time.fixedDeltaTime);
                nextShootTime = Time.time + shootPeriod;
            }
        }
    }
}
