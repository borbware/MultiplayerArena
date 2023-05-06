using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#nullable enable

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
        private ParticleSystem _playerParticles;

        private Player _player;
        private Rigidbody _rigidbody;

        private Vector3 _desiredVelocity;
        private bool jumpReady = true;
        private float thrust = 5f;
        public float knockbackStrength { get; private set; } = 3.5f;

        private Action? singularPhysicsAction = null;
        private Queue<Action> stackingPhysicsActions = new();

        [SerializeField] AudioSource attackAudio;
        [SerializeField] AudioSource isHitAudio;
        

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
            _playerParticles = GetComponent<ParticleSystem>();
        }

        private void FixedUpdate()
        {
            if (singularPhysicsAction != null)
            {
                singularPhysicsAction();
                singularPhysicsAction = null;
            }

            else
            {
                if (_state == State.Dazed)
                    return;

                Move();

                while (stackingPhysicsActions.Count > 0)
                {
                    var physicsAction = stackingPhysicsActions.Dequeue();
                    physicsAction();
                }
            }

            void Move()
            {
                if (_desiredVelocity != Vector3.zero)
                    transform.forward = _desiredVelocity;

                Vector3 calculatedVelocity = _rigidbody.velocity;
                Vector3 velocity = default;

                if (_state == State.Default || _state == State.Iframe)
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
            }
        }

        private void Update()
        {
            if (_player.shootInput){
                Attack();
                attackAudio.Play();
            }
                
            
            if (_player.jumpInput && jumpReady){
                stackingPhysicsActions.Enqueue(Jump);
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
            _rigidbody.AddForce(transform.up * thrust, ForceMode.Impulse);
        }

        public void Daze(Vector3 attackerDirection, float attackerKnockbackStrength)
        {
            if (_state == State.Dazed || _state == State.Iframe)
                return;

            _state = State.Dazed;
            singularPhysicsAction = GetKnockedBack;
            animator.SetTrigger("getHit");
            isHitAudio.Play();
            _playerParticles.Play();

            void GetKnockedBack()
            {
                Vector3 horizontalDirection = -attackerDirection;
                horizontalDirection.y = 0f;

                Vector3 knockbackDirection = horizontalDirection.normalized + transform.up;
                _rigidbody.AddForce(knockbackDirection.normalized * attackerKnockbackStrength, ForceMode.Impulse);
            }
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
