using UnityEngine;

public class Player : MonoBehaviour
{
    public int player;
    public string state = "active"; // hurt, dead
    public Vector2 axisInput;
    public bool jumpInput, shootInput;

	[SerializeField] PlayerUIManager UIManager;

	void Awake()
	{
		// _UImanager = StageManager.instance.UIManagers[player - 1];
	}

    void Update()
	{
		if (StageManager.instance.stageState != StageManager.StageState.Play) // heittää error win	
            return;
		
		if (state == "active")
		{
			axisInput = Vector2.ClampMagnitude(
				new Vector2(
					Input.GetAxisRaw($"P{player}Horizontal"),
					-Input.GetAxisRaw($"P{player}Vertical")
				),
			1f);
			jumpInput = Input.GetButtonDown($"P{player}A");
			shootInput = Input.GetButtonDown($"P{player}B");
		} else if (state == "hurt")
		{
			
		}
	}

	void Hurt(int damage)
	{
		UIManager.AddHP(-damage);
		state = "hurt";
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
		state = "active";
	}
}
