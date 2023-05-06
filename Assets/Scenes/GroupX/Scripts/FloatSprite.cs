using UnityEngine;

namespace GroupX
{
    public class FloatSprite : MonoBehaviour
    {
        public float amplitude = 0.1f;
        public float speed = 1f;

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = startPosition + new Vector3(0f, yOffset, 0f);
        }
    }
}
