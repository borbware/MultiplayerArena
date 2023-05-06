using System.Collections;
using System.Collections.Generic;

using GroupX;

using UnityEngine;

public class MoleScript : MonoBehaviour, IHittableByPlayer
{
    [SerializeField] public float moleLifetime = 3f;
    public int iAmInHoleNo = 0;
    GameObject runningScripts;
    private ParticleSystem moleParticles;

    private bool _dying = false;

    private void OnDestroy()
    {
        // we set call the MoleSpawner script to set the hole the mole was in to empty
        if (iAmInHoleNo < runningScripts.GetComponent<MoleSpawner>().listOfHoles.Count - 1)
        {
            runningScripts.GetComponent<MoleSpawner>().listOfHoles[iAmInHoleNo].setEmpty();
            //Debug.Log($"hole no {iAmInHoleNo} is empty");
        }
    }

    public void GetHitBy(PlayerController player)
    {
        if (!_dying)
        {
            _dying = true;

            runningScripts.GetComponent<MoleSpawner>().playVirusHitAudio();
            GetComponent<MeshRenderer>().enabled = false;
            moleParticles.Play();
            player.GetComponent<Player>().UIManager.AddScore(1);

            Invoke("DestroyThisVirus", 5f);
        }
    }
    void DestroyThisVirus()
    {
        Destroy(gameObject);
    }


    void moveUp()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 1.2f, 0f);
    }

    void moveDown()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, -1.2f, 0f);
    }

    bool stopped = false;
    void checkStopped()
    {
        if (!stopped && transform.position.y >= 0.5)
        {
            stopped = true;
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            Invoke("moveDown", moleLifetime - 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        runningScripts = GameObject.Find("RunningScripts");
        moleParticles = GetComponent<ParticleSystem>();
        moveUp();
    }

    // Update is called once per frame
    void Update()
    {
        checkStopped();
    }
}
