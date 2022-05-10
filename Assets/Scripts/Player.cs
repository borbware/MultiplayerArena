using UnityEngine;
using static GameManager;

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
    public PlayerState state = PlayerState.Active;
    public Vector2 axisInput;
    public bool jumpInput, shootInput;

    public bool hurtDisablesInput;

    PlayerUIManager _UIManager;

    void Start()
    {
        _UIManager = StageManager.instance.UIManagers[player - 1];
        _playerData = GameManager.instance.players[player - 1];
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
    }
    void Hurt(int damage)
    {
        if (state != PlayerState.Active)
            return;
        _UIManager.AddHP(-damage);
        state = PlayerState.Hurt;
        Invoke("Active", 1.0f);
        InvokeRepeating("HurtFlicker",0f,0.1f);
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
