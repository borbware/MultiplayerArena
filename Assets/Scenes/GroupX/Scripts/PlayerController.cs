using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#nullable enable

namespace GroupX
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour, IHittableByPlayer
    {
        [field: SerializeField]
        [field: Range(0f, 100f)]
        public float maxSpeed { get; private set; } = 6f;

        [field: SerializeField]
        [field: Range(0f, 300f)]
        public float maxAcceleration { get; private set; } = 3.5f;

        [field: SerializeField]
        public float maxTurnsPerSecond { get; private set; }

        [field: SerializeField]
        public AimAssist aimAssist { get; private set; }

        [field: SerializeField]
        public float dazeIframeDuration { get; private set; }

        [field: SerializeField]
        public Animator animator { get; private set; }
        private ParticleSystem _playerParticles;

        private Player _player;
        private Rigidbody _rigidbody;

        private Vector3 _desiredVelocity;
        private bool _jumpReady = true;
        private float _thrust = 5f;
        public float knockbackStrength { get; private set; } = 3.5f;

        private Action? _singularPhysicsAction = null;
        private Queue<Action> _stackingPhysicsActions = new();

        [SerializeField] private AudioSource _attackAudio;
        [SerializeField] private AudioSource _isHitAudio;


        private enum State
        {
            Default,
            Dazed,
            Iframe,
            Attacking
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
            if (_singularPhysicsAction != null)
            {
                _singularPhysicsAction();
                _singularPhysicsAction = null;
            }

            else
            {
                if (_state == State.Dazed)
                    return;

                MoveAndRotate();

                while (_stackingPhysicsActions.Count > 0)
                {
                    var physicsAction = _stackingPhysicsActions.Dequeue();
                    physicsAction();
                }
            }

            void MoveAndRotate()
            {
                if (_state == State.Attacking)
                {
                    _rigidbody.velocity = Vector3.zero;
                    return;
                }

                if (!(_state == State.Default || _state == State.Iframe))
                    return;

                if (_desiredVelocity != Vector3.zero)
                {
                    float maxTurnChange = Mathf.PI * maxTurnsPerSecond * Time.fixedDeltaTime;
                    transform.forward = Vector3.RotateTowards(transform.forward, _desiredVelocity.normalized, maxTurnChange, 0f);
                }

                float maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
                Vector3 velocityWithoutRotation = Vector3.MoveTowards(_rigidbody.velocity, _desiredVelocity, maxSpeedChange);
                Vector3 velocity = new(velocityWithoutRotation.x, _rigidbody.velocity.y, velocityWithoutRotation.z);

                bool isWalking = (velocity.x != 0f || velocity.z != 0f);
                animator.SetBool("playerIsWalking", isWalking);

                _rigidbody.velocity = velocity;
            }
        }

        private void Update()
        {
            if (_player.shootInput)
            {
                Attack();
                _attackAudio.Play();
            }


            if (_player.jumpInput && _jumpReady)
            {
                _stackingPhysicsActions.Enqueue(Jump);
                _jumpReady = false;
                Invoke("SetJumpReady", 1f);
            }

            Vector3 movementVector = new(_player.axisInput.x, 0f, _player.axisInput.y);
            _desiredVelocity = movementVector * maxSpeed;

            void Attack()
            {
                _state = State.Attacking;
                animator.SetTrigger("bonk");

                if (aimAssist.TryGetClosest(out var closestTarget))
                {
                    _stackingPhysicsActions.Enqueue(() =>
                    {
                        Vector3 lookDir = closestTarget.transform.position - transform.position;
                        lookDir.y = 0f;
                        transform.forward = lookDir;
                    });
                }
            }

            void Jump() => _rigidbody.AddForce(transform.up * _thrust, ForceMode.Impulse);
        }

        public void EndBonk() => _state = State.Default;

        public void GetHitBy(PlayerController otherPlayer)
        {
            GetDazed(otherPlayer.transform.position - transform.position, otherPlayer.knockbackStrength);

            void GetDazed(Vector3 attackerDirection, float attackerKnockbackStrength)
            {
                if (_state == State.Dazed || _state == State.Iframe)
                    return;

                _state = State.Dazed;
                _singularPhysicsAction = GetKnockedBack;
                animator.SetTrigger("getHit");
                _isHitAudio.Play();
                _playerParticles.Play();

                void GetKnockedBack()
                {
                    Vector3 horizontalDirection = -attackerDirection;
                    horizontalDirection.y = 0f;

                    Vector3 knockbackDirection = horizontalDirection.normalized + transform.up;
                    _rigidbody.AddForce(knockbackDirection.normalized * attackerKnockbackStrength, ForceMode.Impulse);
                }
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
    }
}
