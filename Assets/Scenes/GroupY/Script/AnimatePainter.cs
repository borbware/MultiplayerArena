using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePainter : MonoBehaviour
{
    [SerializeField]Player player;
    [SerializeField]Animator animator;
    Rigidbody rb;

    bool grounded = true, groundedLastFrame = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.jumpInput && grounded)animator.SetTrigger("Jump");
            RaycastHit hit;
            grounded = Physics.SphereCast(transform.position,0.5f, Vector3.down, out hit, 0.5f, Physics.AllLayers);
            Debug.Log("Grounded");
            if(grounded == true && groundedLastFrame == false)animator.SetTrigger("Grounded");    
            groundedLastFrame = grounded;
    }
}
