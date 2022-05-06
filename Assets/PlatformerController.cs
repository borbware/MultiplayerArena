using UnityEngine;

public class PlatformerController : MonoBehaviour
{
	// Start is called before the first frame update
    [SerializeField] int player;
	[SerializeField, Range(0.0f, 100f)] float maxSpeed = 		10f;
	[SerializeField, Range(0.0f, 100f)] float maxAcceleration = 10f;
	[SerializeField, Range(0f,   10f)]	float jumpHeight = 		2f;
	Vector3 velocity, desiredVelocity;
	bool desiredJump;
	Rigidbody _rigidbody;
	bool onGround;

	void OnCollisionStay () {
		onGround = true;
	}
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Death")
        {
            Destroy(gameObject);
            StageManager.instance.AddHP(player, -100);
        }
    }

	// [SerializeField] Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);
	// [SerializeField, Range(0f, 1f)] float bounciness = 0.5f;
	void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 PlayerInput = Vector2.ClampMagnitude(
			new Vector2(
				Input.GetAxisRaw("Horizontal"),
				Input.GetAxisRaw("Vertical")
			),
		1f);
		desiredVelocity = new Vector3(PlayerInput.x, 0f, PlayerInput.y) * maxSpeed;
		desiredJump |= Input.GetButtonDown("Jump");
	}
	private void FixedUpdate() {
		velocity = _rigidbody.velocity;
		float maxSpeedChange = maxAcceleration * Time.deltaTime; // deltatime equals fixedDeltaTime here
		velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
		velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

		if (desiredJump)
		{
			desiredJump = false;
			if (onGround)
				Jump();
		}
		_rigidbody.velocity = velocity;
		onGround = false;
	}
	void Jump() {
		velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
	}
}
