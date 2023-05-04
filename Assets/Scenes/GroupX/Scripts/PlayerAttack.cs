using System;

using UnityEngine;

namespace GroupX
{
    [RequireComponent(typeof(Collider))]
    public class PlayerAttack : MonoBehaviour
    {
        private Player _player;

        private void Awake()
        {
            _player = transform.parent.GetComponent<Player>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<MoleScript>(out var mole))
            {
                Destroy(mole.gameObject);
                _player.UIManager.AddScore(1);
            }

            else if (other.TryGetComponent<PlayerController>(out var player))
            {
                Debug.Log($"Player {player.name} has been hit!");
                throw new NotImplementedException("Players hitting other players not yet implemented");
            }
        }
    }
}