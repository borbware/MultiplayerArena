using UnityEngine;

public class Hurt : MonoBehaviour
{
    [SerializeField] float damage = 10f;

    private void OnCollisionEnter(Collision other) {
        var obj = other.gameObject;
        if (obj.tag == "Player" && obj.GetComponent<Player>().state == "active")
        {
            obj.SendMessage("Hurt", damage);
        }
    }
}
