using UnityEngine;

public class Player : MonoBehaviour
{
	public enum PlayerState
	{
		Active,
		Hurt,
		Dead
	}
    public int player;
    public PlayerState state = PlayerState.Active;
    public Vector2 axisInput;
    public bool jumpInput, shootInput;

	PlayerUIManager _UIManager;

	void Start()
	{
		_UIManager = StageManager.instance.UIManagers[player - 1];
	}

    void Update()
	{
		if (StageManager.instance.stageState != StageManager.StageState.Play) // heittää error win	
            return;
		
		if (state == PlayerState.Active)
		{
			axisInput = Vector2.ClampMagnitude(
				new Vector2(
					Input.GetAxisRaw($"P{player}Horizontal"),
					-Input.GetAxisRaw($"P{player}Vertical")
				),
			1f);
			jumpInput = Input.GetButtonDown($"P{player}A");
			shootInput = Input.GetButtonDown($"P{player}B");
		} else if (state == PlayerState.Hurt)
		{
			
		}
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
