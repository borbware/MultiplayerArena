using System.Collections.Generic;

using UnityEngine;

namespace GroupX
{
    public class ChildrenIgnoreCollision : MonoBehaviour
    {
        [SerializeField] private Collider[] _collidersToIgnore;
        [SerializeField] private bool _childrenIgnoreEachOther = true;

        private void Awake()
        {
            Collider[] childColliders = transform.GetComponentsInChildren<Collider>();

            List<(Collider, Collider)> collisionIgnorePairs = new();

            collisionIgnorePairs.AddRange(PairCollidersWithCollisionsToIgnore(childColliders));

            if (_childrenIgnoreEachOther)
                collisionIgnorePairs.AddRange(PairCollidersWithEachOther(childColliders));

            foreach (var pair in collisionIgnorePairs)
                Physics.IgnoreCollision(pair.Item1, pair.Item2);

            List<(Collider, Collider)> PairCollidersWithCollisionsToIgnore(Collider[] colliders)
            {
                List<(Collider, Collider)> pairs = new();

                foreach (var collider in colliders)
                    foreach (Collider colliderToIgnore in _collidersToIgnore)
                        pairs.Add((collider, colliderToIgnore));

                return pairs;
            }

            // https://stackoverflow.com/questions/1272828/getting-all-the-combinations-in-an-array
            List<(Collider, Collider)> PairCollidersWithEachOther(Collider[] colliders)
            {
                List<(Collider, Collider)> pairs = new();

                for (int i = 0; i < colliders.Length - 1; i++)
                    for (int j = i + 1; j < colliders.Length; j++)
                        pairs.Add((colliders[i], colliders[j]));

                return pairs;
            }
        }
    }
}