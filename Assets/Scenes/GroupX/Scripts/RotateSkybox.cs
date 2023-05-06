using UnityEngine;

namespace GroupX
{
    public class RotateSkybox : MonoBehaviour
    {
        public float rotateSpeed = 1f;

        void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
        }
    }
}
