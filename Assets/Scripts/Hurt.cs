using UnityEngine;

public class Hurt : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    public GameObject shooter;

    private void OnCollisionEnter(Collision other) {
        var obj = other.gameObject;
        if (obj != shooter)
            obj.SendMessage("Hurt", damage, SendMessageOptions.DontRequireReceiver);
    }
}
