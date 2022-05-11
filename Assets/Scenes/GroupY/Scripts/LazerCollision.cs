using UnityEngine;

public class LazerCollision : MonoBehaviour
{
    [SerializeField] float damage = 10f;

    private void OnTriggerEnter(Collider other) {
        var obj = other.gameObject;
        {
            obj.SendMessage("Hurt", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
