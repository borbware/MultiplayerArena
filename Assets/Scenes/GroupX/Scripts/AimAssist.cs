using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GroupX
{
    public class AimAssist : MonoBehaviour
    {
        [SerializeField]
        private Transform _main;

        [SerializeField]
        [Range(-1f, 1f)]
        [Tooltip("-1 = 360 degrees, 0 = 180 degrees, 1 = 0 degrees")]
        private float _attackAngle = 0f;

        private List<Collider> _hittablesWithinTrigger = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IHittableByPlayer>(out _))
                _hittablesWithinTrigger.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_hittablesWithinTrigger.Contains(other))
                _hittablesWithinTrigger.Remove(other);
        }

        public bool TryGetClosest(out Collider closest)
        {
            closest = null;

            _hittablesWithinTrigger.RemoveAll(collider => collider == null);
            if (_hittablesWithinTrigger.Count == 0)
                return false;

            var collidersWithinAttackRange = _hittablesWithinTrigger
                .Where(collider => Vector3.Dot(transform.position, collider.transform.position) >= _attackAngle);
            if (collidersWithinAttackRange.Count() == 0)
                return false;

            Vector2 mainPositionHorizontal = new(_main.transform.position.x, _main.transform.position.z);
            closest = collidersWithinAttackRange
                .OrderBy(collider => collider.TryGetComponent<PlayerController>(out _) ? 1 : 0)
                .ThenBy(collider => GetDistanceSquared(collider, mainPositionHorizontal))
                .First();
            return true;

            static float GetDistanceSquared(Collider x, Vector2 positionHorizontal)
            {
                return (positionHorizontal - new Vector2(x.transform.position.x, x.transform.position.z)).sqrMagnitude;
            }
        }
    }
}