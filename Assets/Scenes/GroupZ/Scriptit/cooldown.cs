using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown : MonoBehaviour
{
    [Header("Cooldown")]
    public Image cdImage;
    public float cooldownTest = 1f;
    bool isCooldown;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Playerrightone").GetComponent<Player>();
        cdImage.fillAmount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown();
    }
    
    void Cooldown()
    {
        if(player.GetComponent<Player>().shootInput && isCooldown == false)
        {
            isCooldown = true;
            cdImage.fillAmount = 1;
        }

        if(isCooldown)
        {
            cdImage.fillAmount -= 1 / cooldownTest * Time.deltaTime;

            if(cdImage.fillAmount <= 0)
            { 
                cdImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
