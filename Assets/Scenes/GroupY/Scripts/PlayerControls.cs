using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	[SerializeField, Range(0.0f, 100f)] float maxSpeed = 		10f;
	[SerializeField, Range(0.0f, 100f)] float maxAcceleration = 10f;
	[SerializeField, Range(0f,   10f)]	float jumpHeight = 		2f;
	Vector3 velocity, desiredVelocity;
	bool desiredJump;

	Rigidbody _rigidbody;
    Player _player;
	bool onGround;

    //bool IsStunned = false;

    bool KnockedBack;

    float[] KnockDir = {1f,1f};

    Rigidbody rb;
    Rigidbody destRB;
    Vector3 KnockbackDir;

	void OnCollisionStay () {
		onGround = true;
	}

	void Start()
	{
        destRB = gameObject.GetComponent<Rigidbody>(); 
        _player = GetComponent<Player>();
		_rigidbody = GetComponent<Rigidbody>();
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
        if(KnockedBack == false){
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
		    _rigidbody.velocity = velocity;} 
        else if (KnockedBack == true) {
            _rigidbody.velocity = KnockbackDir * 220f * Time.fixedDeltaTime;
        }


		onGround = false;
	}
	void Jump() {
		velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
	}

        void OnTriggerEnter(Collider C){
        if(C.gameObject.tag == "Projectile" && KnockedBack == false){
            rb = C.gameObject.GetComponent<Rigidbody>();
            KnockbackDir = rb.velocity.normalized;
            Destroy(C.gameObject);
            _rigidbody.velocity = new Vector3(0f, 0f, 0f);
            KnockedBack = true;
            Invoke("CancelKnockback", 0.15f);
        }
    }

    void CancelKnockback(){
        KnockedBack = false;
    }
}
