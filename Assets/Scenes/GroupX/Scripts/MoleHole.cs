using UnityEngine;

namespace GroupX
{
    public class MoleHole : MonoBehaviour
    {
        /* this clss is a container for a position (Vector3) and a bool which keeps track 
        whether there is a mole already spawned*/

        public Vector3 position;
        public bool isEmpty = true;

        private void Awake()
        {
            position = transform.position;
        }

        public void SetEmpty()
        {
            isEmpty = true;
        }
    }
}
