using System.Collections.Generic;

using UnityEngine;

namespace GroupX
{
    public class MoleSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnInterval = 2;
        [SerializeField] private int _maxMoles = 7; //this should be less than the no of players 
                                           //to create tension (not true - more testing needed)
        [SerializeField] private MoleScript _mole;
        [SerializeField] private GameObject _hole;
        private readonly System.Random _rand = new();

        public MoleHole[] arrayOfHoles;
        [SerializeField] private AudioSource _virusHitAudio;

        public List<MoleHole> listOfHoles = new();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Readonly would be misleading")]
        private List<GameObject> _listofMoles = new();

        private void Awake()
        {
            listOfHoles.AddRange(arrayOfHoles);

            InvokeRepeating(nameof(SpawnMoleInEmptyHole), 3f, _spawnInterval);
        }

        public void PlayVirusHitAudio()
        {
            _virusHitAudio.Play();
        }

        private void SpawnMoleInEmptyHole()
        {
            if (_listofMoles.Count >= _maxMoles)
                return;

            int holeNumber = listOfHoles.IndecesWhere(hole => hole.isEmpty).PickRandomItem();

            MoleScript newMole = Instantiate<MoleScript>(
                _mole,
                listOfHoles[holeNumber].position + new Vector3(0f, -0.1f, 0f),
                Quaternion.Euler(0f, 0f, 0f)
            );
            newMole.occupiedHoleNo = holeNumber;
            Destroy(newMole.gameObject, newMole.moleLifetime);
            _listofMoles.Add(newMole.gameObject);
            newMole.GetComponent<OnDestroyDispatcher>().OnDestroyed += Mole_OnDestroyed;
            listOfHoles[holeNumber].isEmpty = false;

            return;

            void Mole_OnDestroyed(GameObject obj) => _listofMoles.Remove(obj);
        }
    }
}