using UnityEngine;

public class LazerCollision : MonoBehaviour
{
    [SerializeField] float damage = 10f;

    private void OnTriggerEnter(Collider other) {
        var obj = other.gameObject;
        Debug.Log("hi");
        {
            obj.SendMessage("Hurt", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
