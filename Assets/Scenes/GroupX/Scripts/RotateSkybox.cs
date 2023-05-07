using UnityEngine;

namespace GroupX
{
    public class RotateSkybox : MonoBehaviour
    {
        private float _rotateSpeed = 0.3f;

        private void Update()
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * _rotateSpeed);
        }
    }
}
