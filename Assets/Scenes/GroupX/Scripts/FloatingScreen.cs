using UnityEngine;

namespace GroupX
{
    public class FloatingScreen : MonoBehaviour
    {
        public float amplitude = 0.1f;
        public float speed = 1f;
        public float rotationSpeed = 10f;
        public Transform centerObject;

        private Vector3 startPosition;

        void Start()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = startPosition + new Vector3(0f, yOffset, 0f);

            if (centerObject != null)
            {
                Vector3 pivot = centerObject.position;
                Vector3 axis = new Vector3(1f, 0f, 0f);
                float angle = rotationSpeed * Time.deltaTime;
                transform.RotateAround(pivot, axis, angle);
            }
        }
    }
}