using UnityEngine;

public class Player : MonoBehaviour
{
    public int player;
    public Vector2 axisInput;
    public bool jumpInput, shootInput;

    void Update()
	{
		if (StageManager.instance.stageState != "play")
            return;
		axisInput = Vector2.ClampMagnitude(
			new Vector2(
				Input.GetAxisRaw($"P{player}Horizontal"),
				-Input.GetAxisRaw($"P{player}Vertical")
			),
		1f);
		jumpInput = Input.GetButtonDown($"P{player}A");
		shootInput = Input.GetButtonDown($"P{player}B");
	}
}
