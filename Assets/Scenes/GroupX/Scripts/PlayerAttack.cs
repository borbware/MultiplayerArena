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
            Debug.Log(_player.name);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<mole_script>(out var mole))
            {
                Debug.Log("Mole hit");
                Destroy(mole.gameObject);
                _player.UIManager.AddScore(1);
            }

            else if (other.TryGetComponent<PlayerController>(out var player))
            {
                Debug.Log($"Player {player.name} has been hit!");
            }
        }
    }
}