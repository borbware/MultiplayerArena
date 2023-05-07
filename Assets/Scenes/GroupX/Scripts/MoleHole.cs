using UnityEngine;

namespace GroupX
{
    /// <summary>
    /// Container for position & a bool that keeps track if a mole's already spawned
    /// </summary>
    public class MoleHole : MonoBehaviour
    {
        public Vector3 position;
        public bool isEmpty = true;

        private void Awake()
        {
            position = transform.position;
        }

        public void SetEmpty() => isEmpty = true;
    }
}
