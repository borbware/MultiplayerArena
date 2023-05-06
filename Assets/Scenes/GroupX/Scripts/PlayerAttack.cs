using UnityEngine;

namespace GroupX
{
    [RequireComponent(typeof(Collider))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<MoleScript>(out var mole))
            {
                Destroy(mole.gameObject);
                _player.UIManager.AddScore(1);
            }

            else if (other.TryGetComponent<PlayerController>(out var otherPlayer))
            {
                otherPlayer.Daze(_player.transform.position - otherPlayer.transform.position);
            }
        }
    }
}