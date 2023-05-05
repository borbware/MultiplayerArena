using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

namespace GroupZ
{
public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Active,
        Hurt,
        Dead
    }
    public int player;
    PlayerData _playerData;
    public Rigidbody rb;
    public PlayerState state = PlayerState.Active;
    public Vector2 axisInput;
    public bool jumpInput, shootInput, continuousJumpInput, continuousShootInput;

    public bool hurtDisablesInput;
    [SerializeField] float swingInputremovetime = 0.4f;

    public PlayerUIManager UIManager;
    PlatformerController _platformerController;
    Animator _anim;

    void Start()
    {
        UIManager = StageManager.instance.UIManagers[player - 1];
        _playerData = GameManager.instance.players[player - 1];
        rb = GetComponent<Rigidbody>();
        _platformerController = GetComponent<PlatformerController>();
        _anim = transform.GetChild(0).GetComponent<Animator>();
        hurtDisablesInput = true;
    }

    void Update()
    {
        if (StageManager.instance.stageState != StageManager.StageState.Play) // heittää error win	
            return;
        
        if (state == PlayerState.Active)
        {
            GetInput();
        } else if (state == PlayerState.Hurt)
        {
            if (!hurtDisablesInput)
                GetInput();
        }
        _anim.SetBool("onGround", _platformerController.onGround);
        _anim.SetBool("isMoving", (axisInput.magnitude > 0));
        if(jumpInput)
        {
            _anim.Play("FrogoJumpStart");
        }
    }

    void GetInput()
    {
        if (_playerData.controller == 0)
            return;
        axisInput = Vector2.ClampMagnitude(
            new Vector2(
                Input.GetAxisRaw($"P{_playerData.controller}Horizontal"),
                -Input.GetAxisRaw($"P{_playerData.controller}Vertical")
            ),
        1f);
        jumpInput = Input.GetButtonDown($"P{_playerData.controller}A");
        shootInput = Input.GetButtonDown($"P{_playerData.controller}B");

        continuousJumpInput = Input.GetButton($"P{_playerData.controller}A");
        continuousShootInput = Input.GetButton($"P{_playerData.controller}B");

    }
    public void Hurt(int damage)
    {
        if (state != PlayerState.Active)
            return;
        UIManager.AddHP(-damage);
        state = PlayerState.Hurt;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Invoke("Active", swingInputremovetime);
        //InvokeRepeating("HurtFlicker",0f,0.1f);
    }
    void HurtFlicker()
    {
        var _renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in _renderers)
            renderer.enabled = !renderer.enabled;
    }
    void Active()
    {
        CancelInvoke("HurtFlicker");
        state = PlayerState.Active;
    }
}
    
}
