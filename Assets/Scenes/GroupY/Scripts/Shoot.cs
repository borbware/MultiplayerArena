using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{

    bool desiredShoot;
    float nextShootTime;
    [SerializeField, Range(0.01f,   10f)] float shootPeriod;
    [SerializeField, Range(0f,   10000f)] float shootForce;
    [SerializeField] GameObject bullet, AutoBullet;

    public PlayerUIManager UIManager;

    [SerializeField]
    int PlayerIndex;

    Player _player;
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

    void Update(){
        if (StageManager.instance.stageState != StageManager.StageState.Play)
            return;
        if (_player != null)
        {
            //autoFire |= _player.continuousShootInput && UIManager.score > 0;
		    desiredShoot |= _player.shootInput;// && UIManager.score < 1;
        } else {
            desiredShoot = true;
            //autoFire = true;
        }

        // if(LastChargePress + ChargeCD < Time.time){
        //     ChargeGain();
        
    }

    void FixedUpdate(){
        if (desiredShoot && UIManager.score > 0){
            desiredShoot = false;
            Ammu(AutoBullet, 1.2f, true, 0.35f);

        }
        else if (desiredShoot)
        {
            desiredShoot = false;
            Ammu(bullet, 0.7f, false, 1f);
        }
    }


    void Ammu(GameObject Luoti, float LuotiSpeed, bool DeductScore, float Modifier){
        if (Luoti != null && Time.time >= nextShootTime){
            LastChargePress = Time.time;
            var newBullet = Instantiate(
                Luoti,
                transform.position + transform.forward * LuotiSpeed,
                Quaternion.identity);
            if(DeductScore == true){UIManager.AddScore(-1);}
            Destroy(newBullet, 5);
            newBullet.GetComponent<Rigidbody>().AddForce(
                transform.forward * (shootForce * Modifier) * Time.fixedDeltaTime);
            nextShootTime = Time.time + shootPeriod;
        }
    }


    void OnCollisionEnter(Collision C){
        if(C.gameObject.tag == "PowerUp_AutoShot" && CanPickUp != false){
            Destroy(C.gameObject);
            UIManager.AddScore(10);
            Invoke("CanPickUpAgain", 0.05f);
            CanPickUp = false;
        }else if (C.gameObject.tag == "HealPickUp" && CanPickUp != false){
            Destroy(C.gameObject);
            if(UIManager.hp < 76 && UIManager.hp > 0){
                UIManager.AddHP(25);}
            else if (UIManager.hp > 0){
                UIManager.AddHP((100f - UIManager.hp));
            }
        }
    }

    void CanPickUpAgain(){
        CanPickUp = true;
    }
}
