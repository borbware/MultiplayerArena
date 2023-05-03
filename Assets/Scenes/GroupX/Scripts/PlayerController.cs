using System;

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
        public float maxSpeed { get; private set; } = 10f;

        [field: SerializeField]
        [field: Range(0f, 100f)]
        public float maxAcceleration { get; private set; } = 10f;

        private Player _player;
        private Rigidbody _rigidbody;
        private Animator _animator;

        private Vector3 _desiredVelocity;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            float maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
            Vector3 velocity = Vector3.MoveTowards(_rigidbody.velocity, _desiredVelocity, maxSpeedChange);
            _rigidbody.velocity = velocity;
        }

        private void Update()
        {
            if (_player.shootInput)
                Attack();

            Vector3 movementVector = new(_player.axisInput.x, 0f, _player.axisInput.y);
            if (movementVector != Vector3.zero)
                transform.forward = movementVector;

            _desiredVelocity = movementVector * maxSpeed;
        }

        private void Attack()
        {
            _animator.SetTrigger("Attack");
        }
    }
}
