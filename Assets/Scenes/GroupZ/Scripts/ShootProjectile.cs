using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupZ
{
public class ShootProjectile : MonoBehaviour
{
    bool desiredShoot;
    float nextShootTime, shootRange = 1f;
    [Range(0.01f,   10f)] public float shootPeriod;
    [Range(0f,   10000f)] public float shootForce;
    public GameObject bullet;

    Player _player;
    Animator _anim;
    PlatformerController _platformerController;
    AudioSource audioUse;
    public AudioClip hammerSwoosh;
    public AudioClip hammerHit;
    ParticleSystem.EmissionModule debrisEmission;

    private void Start()
    {
        _player = GetComponent<Player>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
        _platformerController = GetComponent<PlatformerController>();
        audioUse = GetComponent<AudioSource>();
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

    void WhooshSound()
    {
        audioUse.PlayOneShot(hammerSwoosh, 1f);
    }

    void ThumpSound()
    {
        if (_platformerController.onGround)
            audioUse.PlayOneShot(hammerHit, 1f);
    }
    void HammerHit()
    {
        var newBullet = Instantiate(
        bullet,
        transform.position + transform.forward * shootRange,
        Quaternion.identity
        );
        newBullet.GetComponent<ShockWave>().shooter = _player.player;

        ParticleSystemRenderer particleRender = newBullet.GetComponent<ParticleSystemRenderer>();
        GameObject debris = newBullet.transform.GetChild(1).gameObject;
        debrisEmission = debris.GetComponent<ParticleSystem>().emission;
        if (!_platformerController.onGround)
        {
            particleRender.enabled = false;
            debrisEmission.enabled = false;
        } else {
            particleRender.enabled = true;
            debrisEmission.enabled = true;
        }

        // if want projectile to hurt the target it touches
        Hurt _hurt = newBullet.GetComponent<Hurt>();
        if (_hurt != null)
            _hurt.shooter = gameObject;
        newBullet.GetComponent<Rigidbody>().AddForce(
            transform.forward * shootForce * Time.fixedDeltaTime);
        Destroy(newBullet, shootPeriod);
        nextShootTime = Time.time + shootPeriod;
    }
    void FixedUpdate()
    {
        if (desiredShoot)
        {
            desiredShoot = false;
            if (bullet != null && Time.time >= nextShootTime)
            {
                if(_anim != null)
                {
                    _anim.Play("FrogoSmash");
                }
                Invoke("HammerHit", 0.2f);
                Invoke("ThumpSound", 0.08f);
                Invoke("WhooshSound", 0.03f);
                _player.rb.velocity = Vector3.zero;
                _player.rb.angularVelocity = Vector3.zero;
                

                _player.Hurt(0);
            }
        }
    }
}
}
