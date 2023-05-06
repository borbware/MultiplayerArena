using UnityEngine;

namespace GroupX
{
    [RequireComponent(typeof(Collider))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = _player.GetComponent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IHittableByPlayer>(out var hittable))
                hittable.GetHitBy(_playerController);
        }
    }
}