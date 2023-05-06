using System.Collections;

using GroupX;

using UnityEngine;

namespace GroupX
{
    public class ForwardAnimationEventsToPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerController player;

        public void EndDaze() => player.EndDaze();

        public void EndBonk() => player.ResetState();
    }
}