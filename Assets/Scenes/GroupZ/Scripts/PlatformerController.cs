using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroupZ
{
public class PlatformerController : MonoBehaviour
{
    [Range(0.0f, 100f)] public float maxSpeed = 		10f;
	[Range(0.0f, 100f)] public float maxAcceleration = 10f;
	[Range(0f,   10f)]  public float jumpHeight = 		2f;
	Vector3 velocity, desiredVelocity;
	bool desiredJump;

	Rigidbody _rigidbody;
    Player _player;
	public bool onGround;

	AudioSource audioOut;
	public AudioClip jump;

	void OnCollisionStay () {
		onGround = true;
	}

	void Start()
	{
        _player = GetComponent<Player>();
		_rigidbody = GetComponent<Rigidbody>();
		audioOut = GetComponent<AudioSource>();
	}

    private void Update() {
        desiredVelocity = new Vector3(_player.axisInput.x, 0f, _player.axisInput.y) * maxSpeed;
        if (desiredVelocity.sqrMagnitude > 0.1)
        {
            transform.forward = new Vector3(_player.axisInput.x, 0, _player.axisInput.y);
        }
		desiredJump |= _player.jumpInput;
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
		audioOut.PlayOneShot(jump, 1f);
	}
}
}
