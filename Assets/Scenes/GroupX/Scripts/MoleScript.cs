using UnityEngine;

namespace GroupX
{
    [RequireComponent(typeof(OnDestroyDispatcher))]
    public class MoleScript : MonoBehaviour, IHittableByPlayer
    {
        public float moleLifetime = 3f;
        public int occupiedHoleNo { get; set; } = 0;
        private MoleSpawner _moleSpawner;
        private ParticleSystem _moleParticles;

        private bool _dying = false;
        private bool _stopped = false;

        private void Awake()
        {
            _moleSpawner = FindObjectOfType<MoleSpawner>();
            _moleParticles = GetComponent<ParticleSystem>();
            MoveUp();
        }

        private void Update()
        {
            StopIfFinishedMovingUp();
        }

        private void OnDestroy()
        {
            if (occupiedHoleNo < _moleSpawner.listOfHoles.Count - 1)
            {
                _moleSpawner.listOfHoles[occupiedHoleNo].SetEmpty();
            }
        }

        public void GetHitBy(PlayerController player)
        {
            if (_dying)
                return;

            _dying = true;

            _moleSpawner.PlayVirusHitAudio();
            GetComponent<MeshRenderer>().enabled = false;
            _moleParticles.Play();
            player.GetComponent<Player>().UIManager.AddScore(1);

            Invoke(nameof(DestroyThisVirus), 5f);
        }

        private void DestroyThisVirus() => Destroy(gameObject);

        private void MoveUp() => GetComponent<Rigidbody>().velocity = new Vector3(0f, 1.2f, 0f);

        private void MoveDown() => GetComponent<Rigidbody>().velocity = new Vector3(0f, -1.2f, 0f);

        private void StopIfFinishedMovingUp()
        {
            if (_stopped || transform.position.y < 0.5)
                return;

            _stopped = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            Invoke(nameof(MoveDown), moleLifetime - 1);
        }
    }
}
