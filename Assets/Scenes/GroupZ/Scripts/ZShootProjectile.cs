using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZShootProjectile : MonoBehaviour
{
    bool desiredShoot;
    float nextShootTime, shootRange = 1f;
    [Range(0.01f,   10f)] public float shootPeriod;
    [Range(0f,   10000f)] public float shootForce;
    public GameObject bullet;

    ZPlayer _player;
    private void Start()
    {
        _player = GetComponent<ZPlayer>();
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
                    transform.position + transform.forward * shootRange,
                    Quaternion.identity
                );
                // if want projectile to hurt the target it touches
                Hurt _hurt = newBullet.GetComponent<Hurt>();
                if (_hurt != null)
                    _hurt.shooter = gameObject;
                newBullet.GetComponent<Rigidbody>().AddForce(
                    transform.forward * shootForce * Time.fixedDeltaTime);
                Destroy(newBullet, shootPeriod);
                nextShootTime = Time.time + shootPeriod;
            }
        }
    }
}
