using UnityEngine;

namespace GroupX
{
    public class ForwardAnimationEventsToPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;

        public void EndDaze() => _player.EndDaze();

        public void PlayBonkAudio() => _player.PlayBonkAudio();
        public void EndBonk() => _player.EndBonk();
    }
}