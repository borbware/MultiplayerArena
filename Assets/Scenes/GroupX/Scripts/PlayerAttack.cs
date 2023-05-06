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
            if (other.TryGetComponent<MoleScript>(out var mole))
            {
                Destroy(mole.gameObject);
                _player.UIManager.AddScore(1);
            }

            else if (other.TryGetComponent<PlayerController>(out var otherPlayerController))
            {
                otherPlayerController.Daze(
                    _playerController.transform.position - otherPlayerController.transform.position,
                    _playerController.knockbackStrength);
            }
        }
    }
}