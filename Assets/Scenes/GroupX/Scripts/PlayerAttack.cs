using UnityEngine;

namespace GroupX
{
    [RequireComponent(typeof(Collider))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IHittableByPlayer>(out var hittable))
                hittable.GetHitBy(_player);
        }
    }
}