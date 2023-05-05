using System;
using System.Collections;

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
        

        private enum State
        {
            Default,
            Dazed
        }
        private State _state = State.Default;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            Vector3 calculatedVelocity = _rigidbody.velocity;
            Vector3 velocity = default;

            if (_state == State.Dazed)
            {
                velocity = _desiredVelocity;
            }
            else if (_state == State.Default)
            {
                float maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
                velocity = Vector3.MoveTowards(_rigidbody.velocity, _desiredVelocity, maxSpeedChange);
            }

            velocity.y = calculatedVelocity.y;
            _rigidbody.velocity = velocity;

            if (_state == State.Default)
                Jump();
        }

        private void Update()
        {
            if (_state == State.Dazed)
            {
                _desiredVelocity = Vector3.zero;
                return;
            }

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
            _animator.SetTrigger("Attack"); // TODO remove once actual animations are in
            StartCoroutine(SetAnimatorBoolOnFor("playerIsAttacking", null));
        }

        private void Jump(){
            if (isJumping){
                //Debug.Log("i jumped");
                _rigidbody.AddForce(transform.up * thrust, ForceMode.Impulse);
                isJumping = false;
            }
        }

        public void Daze()
        {
            _state = State.Dazed;
            StartCoroutine(SetAnimatorBoolOnFor("playerIsHit", null));
            Invoke(nameof(Undaze), 5f);

        }
        private void Undaze() => _state = State.Default;

        private IEnumerator SetAnimatorBoolOnFor(string animatorBoolName, YieldInstruction duration)
        {
            _animator.SetBool(animatorBoolName, true);
            yield return duration;
            _animator.SetBool(animatorBoolName, false);
        }
    
        private void SetJumpReady(){
            jumpReady = true;
        }
    }
}
