using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{

    bool desiredShoot;
    bool autoFire;
    float nextShootTime;
    [SerializeField, Range(0.01f,   10f)] float shootPeriod;
    [SerializeField, Range(0f,   10000f)] float shootForce;
    [SerializeField] GameObject bullet, AutoBullet;

    public PlayerUIManager UIManager;

    [SerializeField]
    int PlayerIndex;

    Player _player;

    int MaxCharges = 5;
    float ChargeCD = 2.3f;
    float ChargeTimer;
    float ChargeStart;
    float LastChargePress = 0;

    bool CanPickUp;

    GameObject scoreObj;
    Text scoreText;

    private void Start()
    {
        UIManager = StageManager.instance.UIManagers[PlayerIndex - 1];
        _player = GetComponent<Player>();
        CanPickUp = true;
        //scoreObj = transform.Find("Score").gameObject;
        //scoreText = scoreObj.GetComponent<Text>();
    }

    void ChargeGain(){
        if((ChargeTimer + (ChargeCD/7) < Time.time) 
        && UIManager.score < MaxCharges){
           UIManager.AddScore(1);
           ChargeTimer = Time.time;
        }
    }

    void Update(){
        if (StageManager.instance.stageState != StageManager.StageState.Play)
            return;
        if (_player != null)
        {
            autoFire |= _player.continuousShootInput && UIManager.score > 0;
		    desiredShoot |= _player.shootInput && UIManager.score < 1;
        } else {
            desiredShoot = true;
            autoFire = true;
        }

        // if(LastChargePress + ChargeCD < Time.time){
        //     ChargeGain();
        
    }

    void FixedUpdate(){
        if (autoFire){
            autoFire = false;
            Ammu(AutoBullet, 0.6f, 0.5f, true);

        }
        else if (desiredShoot)
        {
            desiredShoot = false;
            Ammu(bullet, 0.7f, 1, false);
        }
    }


    void Ammu(GameObject Luoti, float LuotiSpeed, float Modifier, bool DeductScore){
        if (Luoti != null && Time.time >= nextShootTime){
            LastChargePress = Time.time;
            var newBullet = Instantiate(
                Luoti,
                transform.position + transform.forward * LuotiSpeed,
                Quaternion.identity);
            if(DeductScore == true){UIManager.AddScore(-1);}
            Destroy(newBullet, 5);
            newBullet.GetComponent<Rigidbody>().AddForce(
                transform.forward * shootForce * Time.fixedDeltaTime);
            nextShootTime = Time.time + shootPeriod * Modifier;
        }
    }


    void OnCollisionEnter(Collision C){
        if(C.gameObject.tag == "PowerUp_AutoShot" && CanPickUp != false){
            Destroy(C.gameObject);
            UIManager.AddScore(10);
            Invoke("CanPickUpAgain", 0.05f);
            CanPickUp = false;
        }
    }

    void CanPickUpAgain(){
        CanPickUp = true;
    }
}
