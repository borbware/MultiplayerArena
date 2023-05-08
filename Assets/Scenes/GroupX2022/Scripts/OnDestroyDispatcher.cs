using UnityEngine;

#nullable enable

namespace GroupX
{
    public class OnDestroyDispatcher : MonoBehaviour
    {
        public event System.Action<GameObject>? OnDestroyed;
        private void OnDestroy() => OnDestroyed?.Invoke(gameObject);
    }
}