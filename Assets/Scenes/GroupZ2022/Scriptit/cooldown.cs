using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldown : MonoBehaviour
{
    [Header("Cooldown")]
    public Image cdImage;
    public bool isCooldown = false;
    Player player;
    float math;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        cdImage.fillAmount = 0;
        math = 1/2.35f;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.shootInput && isCooldown == false)
        {
            isCooldown = true;
            cdImage.fillAmount = 1;
        }

        if(isCooldown)
        {
            cdImage.fillAmount -= math * Time.deltaTime;

            if(cdImage.fillAmount <= 0)
            { 
                cdImage.fillAmount = 0;
                isCooldown = false;
            }
        }
        

        
    }
    
    void Cooldown()
    {

    }
}
