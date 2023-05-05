using System;
using System.Threading;
using UnityEngine;

namespace GroupX
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField]
        [field: Range(0f, 100f)]
        public float maxSpeed { get; private set; } = 6f;

        [field: SerializeField]
        [field: Range(0f, 300f)]
        public float maxAcceleration { get; private set; } = 3.5f;

        private Player _player;
        private Rigidbody _rigidbody;
        private Animator _animator;

        private Vector3 _desiredVelocity;
        private bool isJumping = false;
        private bool jumpReady = true;
        private float thrust = 5f;
        

        private void Awake()
        {
            _player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            float maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
            Vector3 calculatedVelocity = _rigidbody.velocity;
            Vector3 velocity = Vector3.MoveTowards(_rigidbody.velocity, _desiredVelocity, maxSpeedChange);
            velocity.y = calculatedVelocity.y;
            _rigidbody.velocity = velocity;

            Jump();
        }

        private void Update()
        {
            if (_player.shootInput)
                Attack();
            
            if (_player.jumpInput && jumpReady){
                isJumping = true;
                jumpReady = false;
                Invoke("SetJumpReady", 1f);
            }

            Vector3 movementVector = new(_player.axisInput.x, 0f, _player.axisInput.y);
            if (movementVector != Vector3.zero)
                transform.forward = movementVector;

            _desiredVelocity = movementVector * maxSpeed;
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        private void Jump(){
            if (isJumping){
                //Debug.Log("i jumped");
                _rigidbody.AddForce(transform.up * thrust, ForceMode.Impulse);
                isJumping = false;
            }
            
        }
    
        private void SetJumpReady(){
            jumpReady = true;
        }
    }
}
