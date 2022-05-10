using UnityEngine;

public class Hurt : MonoBehaviour
{
    [SerializeField] float damage = 10f;

    private void OnCollisionEnter(Collision other) {
        var obj = other.gameObject;
        {
            obj.SendMessage("Hurt", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
