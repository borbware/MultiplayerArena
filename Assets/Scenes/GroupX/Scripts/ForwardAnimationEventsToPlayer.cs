using System.Collections;

using GroupX;

using UnityEngine;

namespace GroupX
{
    public class ForwardAnimationEventsToPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;

        public void EndDaze() => _player.EndDaze();

        public void EndBonk() => _player.ResetState();
    }
}