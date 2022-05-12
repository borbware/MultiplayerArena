using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Shoot : MonoBehaviour
{

    bool desiredShoot;
    float nextShootTime;
    [SerializeField, Range(0.01f,   10f)] float shootPeriod;
    [SerializeField, Range(0f,   10000f)] float shootForce;
    [SerializeField] GameObject bullet;

    public PlayerUIManager UIManager;

    [SerializeField]
    int PlayerIndex;

    Player _player;

    int MaxCharges = 5;
    float ChargeCD = 2.3f;
    float ChargeTimer;
    float ChargeStart;
    float LastChargePress = 0;



    void Awake(){
            //UIManagers.Add(UIManager.GetComponent<PlayerUIManager>());
    }
    private void Start()
    {
        UIManager = StageManager.instance.UIManagers[PlayerIndex - 1];
        UIManager.AddScore(5);
        _player = GetComponent<Player>();
    }

    void ChargeGain(){
        if((ChargeTimer + (ChargeCD/7) < Time.time) 
        && UIManager.score < MaxCharges){
           UIManager.AddScore(1);
           ChargeTimer = Time.time;
        }
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

        if(LastChargePress + ChargeCD < Time.time){
            ChargeGain();
        }
    }

    void FixedUpdate()
    {
        Debug.Log(UIManager.score);
        if (desiredShoot && UIManager.score > 0)
        {
            desiredShoot = false;
            if (bullet != null && Time.time >= nextShootTime)
            {
                LastChargePress = Time.time;
                UIManager.AddScore(-1);
                var newBullet = Instantiate(
                    bullet,
                    transform.position + transform.forward * 0.7f,
                    Quaternion.identity
                );
                newBullet.GetComponent<Rigidbody>().AddForce(
                    transform.forward * shootForce * Time.fixedDeltaTime);
                nextShootTime = Time.time + shootPeriod;
            }
        }
    }
}
