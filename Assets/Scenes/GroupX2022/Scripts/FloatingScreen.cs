using UnityEngine;

namespace GroupX
{
    public class FloatingScreen : MonoBehaviour
    {
        public float amplitude = 0.1f;
        public float speed = 1f;
        public float rotationSpeed = 10f;
        public Transform centerObject;

        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = _startPosition + new Vector3(0f, yOffset, 0f);

            if (centerObject != null)
            {
                Vector3 pivot = centerObject.position;
                Vector3 axis = new(0f, 1f, 2f);
                float angle = rotationSpeed * Time.deltaTime;
                transform.RotateAround(pivot, axis, angle);
            }
        }
    }
}
