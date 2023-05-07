using System.Collections;
using System.Collections.Generic;

using GroupX;

using UnityEngine;

[RequireComponent(typeof(OnDestroyDispatcher))]
public class MoleScript : MonoBehaviour, IHittableByPlayer
{
    public float moleLifetime = 3f;
    public int iAmInHoleNo = 0;
    private GameObject _runningScripts;
    private ParticleSystem _moleParticles;

    private bool _dying = false;

    private void OnDestroy()
    {
        // we set call the MoleSpawner script to set the hole the mole was in to empty
        if (iAmInHoleNo < _runningScripts.GetComponent<MoleSpawner>().listOfHoles.Count - 1)
        {
            _runningScripts.GetComponent<MoleSpawner>().listOfHoles[iAmInHoleNo].SetEmpty();
            //Debug.Log($"hole no {iAmInHoleNo} is empty");
        }
    }

    public void GetHitBy(PlayerController player)
    {
        if (!_dying)
        {
            _dying = true;

            _runningScripts.GetComponent<MoleSpawner>().PlayVirusHitAudio();
            GetComponent<MeshRenderer>().enabled = false;
            _moleParticles.Play();
            player.GetComponent<Player>().UIManager.AddScore(1);

            Invoke(nameof(DestroyThisVirus), 5f);
        }
    }

    private void DestroyThisVirus()
    {
        Destroy(gameObject);
    }

    private void moveUp()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 1.2f, 0f);
    }

    private void moveDown()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, -1.2f, 0f);
    }

    private bool _stopped = false;

    private void checkStopped()
    {
        if (!_stopped && transform.position.y >= 0.5)
        {
            _stopped = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            Invoke(nameof(moveDown), moleLifetime - 1);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _runningScripts = GameObject.Find("RunningScripts");
        _moleParticles = GetComponent<ParticleSystem>();
        moveUp();
    }

    // Update is called once per frame
    private void Update()
    {
        checkStopped();
    }
}
