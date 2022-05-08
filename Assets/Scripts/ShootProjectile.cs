using UnityEngine;

public class ShootProjectile : MonoBehaviour
{

    bool desiredShoot;
    float nextShootTime;
    [SerializeField, Range(0.01f,   10f)] float shootPeriod;
    [SerializeField, Range(0f,   10000f)] float shootForce;
    [SerializeField] GameObject bullet;

    Player _player;
    private void Start()
    {
        _player = GetComponent<Player>();
    }
    void Update()
    {
        if (StageManager.instance.stageState != "play")
            return;
        if (_player != null)
        {
		    desiredShoot |= _player.shootInput;
        } else {
            desiredShoot = true;
        }
    }

    // Update is called once per frame
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
