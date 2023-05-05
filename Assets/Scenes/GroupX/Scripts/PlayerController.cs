using System;
using System.Collections;

using UnityEngine;

namespace GroupX
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField]
        [field: Range(0f, 100f)]
        public float maxSpeed { get; private set; } = 6f;

        [field: SerializeField]
        [field: Range(0f, 300f)]
        public float maxAcceleration { get; private set; } = 3.5f;

        [field: SerializeField]
        public float dazeIframeDuration { get; private set; }

        [field: SerializeField]
        public Animator animator { get; private set; }

        private Player _player;
        private Rigidbody _rigidbody;

        private Vector3 _desiredVelocity;
        private bool isJumping = false;
        private bool jumpReady = true;
        private float thrust = 5f;

        private enum State
        {
            Default,
            Dazed,
            Iframe
        }
        private State _state = State.Default;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_desiredVelocity != Vector3.zero)
                transform.forward = _desiredVelocity;

            Vector3 calculatedVelocity = _rigidbody.velocity;
            Vector3 velocity = default;

            if (_state == State.Dazed)
            {
                velocity = _desiredVelocity;
            }
            else if (_state == State.Default || _state == State.Iframe)
            {
                float maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
                velocity = Vector3.MoveTowards(_rigidbody.velocity, _desiredVelocity, maxSpeedChange);

                if (velocity.x == 0f || velocity.z == 0f)
                    animator.SetBool("playerIsWalking", false);
                else
                    animator.SetBool("playerIsWalking", true);
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
            _desiredVelocity = movementVector * maxSpeed;
        }

        private void Attack()
        {
            animator.SetTrigger("bonk");
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
            if (_state == State.Dazed || _state == State.Iframe)
                return;

            _state = State.Dazed;
            animator.SetTrigger("getHit");
        }

        public void EndDaze()
        {
            StartCoroutine(SetDefaultStateAfterIframeDuration());

            IEnumerator SetDefaultStateAfterIframeDuration()
            {
                _state = State.Iframe;
                yield return new WaitForSeconds(dazeIframeDuration);
                _state = State.Default;
            }
        }

        private void SetJumpReady(){
            jumpReady = true;
        }
    }
}
