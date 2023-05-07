using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GroupX
{
    public class FloorDropEvent : MonoBehaviour
    {
        private double _timeTillDrop;
        private double _totalStageTime;
        private float _dropAfterPercent = 0.6f;  //the stage drop after this amount of the total stage time has elapsed
        private bool _eventTriggered = false;
        private bool _warningSounded = false;

        [SerializeField] private AudioSource _dropWarningAudio;
        [SerializeField] private AudioSource _floorDropAudio;
        [SerializeField] private AudioSource _musicAudio;
        [SerializeField] private AudioClip _musicSpedUpClip;
        [SerializeField] private List<GameObject> _outerHexes;
        private StageManager _stageManager;
        private List<MeshRenderer> _meshRenderers = new();

        //feel free to change these 4 vectors and experiment with them
        private Vector3 _mainCameraFarPosition = new Vector3(-339.5f, -200f, 41f);
        private Quaternion _mainCameraFarRotation = Quaternion.Euler(66.5f, 0f, 0f);
        private Vector3 _mainCameraClosePosition = new Vector3(-340f, -207f, 43f);
        private Quaternion _mainCameraCloseRotation = Quaternion.Euler(45f, 0f, 0f);
        private GameObject _mainCamera;
        private float _lerpTimePassed = 0f;
        private float _lerpDuration = 3f;

        // Start is called before the first frame update
        private void Start()
        {
            _stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
            _totalStageTime = _stageManager.stageTime;
            _timeTillDrop = _totalStageTime * _dropAfterPercent;

            foreach (GameObject hex in _outerHexes)
                _meshRenderers.AddRange(hex.GetComponentsInChildren<MeshRenderer>());

            _mainCamera = GameObject.Find("Main Camera");
        }

        // Update is called once per frame
        private void Update()
        {
            eventTrigger();
            moveCamera();
            getCameraPosition();
        }

        private void eventTrigger()
        {
            float currentStageTime = _stageManager.stageTime;

            //warning sounds & colors
            if (_totalStageTime - currentStageTime >= _timeTillDrop - 2.9 && !_warningSounded)
            {
                _warningSounded = true;
                InvokeRepeating("playWarningSound", 0.1f, 1f);
                foreach (var meshRenderer in _meshRenderers)
                    StartCoroutine(FlashReddish(meshRenderer));
            }

            //the event
            if (_totalStageTime - currentStageTime > _timeTillDrop && !_eventTriggered)
            {
                _eventTriggered = true;

                //remove the extra moleholes - removes 6 elements starting from index 7 (these are the last 6 holes)
                GetComponent<MoleSpawner>().listOfHoles.RemoveRange(7, 6);

                //drop outer hexes
                _floorDropAudio.Play();
                foreach (GameObject hex in _outerHexes)
                {
                    hex.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    hex.GetComponent<Rigidbody>().useGravity = true;
                }

                //change music to sped up version
                _musicAudio.Pause();
                float audioPosition = Mathf.InverseLerp(0, _musicAudio.clip.samples, _musicAudio.timeSamples);
                _musicAudio.clip = _musicSpedUpClip;
                _musicAudio.timeSamples = Mathf.RoundToInt(Mathf.Lerp(0, _musicAudio.clip.samples, audioPosition));
                _musicAudio.Play();
            }
        }

        private void playWarningSound()
        {
            if (!_eventTriggered) { _dropWarningAudio.Play(); }
        }

        private IEnumerator FlashReddish(MeshRenderer meshRenderer)
        {
            float startTime = Time.time;
            Material material = meshRenderer.material;
            Color originalColor = material.color;

            Color reddish = Color.Lerp(originalColor, Color.red, 0.3f);

            while (!_eventTriggered)
            {
                float delta = (Time.time - startTime) % 1f;
                float deltaDiffFromHalf = Mathf.Abs(0.5f - delta);
                float howFarFromReddish = Mathf.InverseLerp(0, 0.5f, deltaDiffFromHalf);
                material.color = Color.Lerp(reddish, originalColor, howFarFromReddish);
                yield return null;
            }

            material.color = originalColor;
        }

        private void moveCamera()
        {
            if (_eventTriggered && _mainCamera.transform.position != _mainCameraClosePosition)
            {
                if (_lerpTimePassed < _lerpDuration)
                {
                    _mainCamera.transform.localPosition =
                        Vector3.Lerp(_mainCameraFarPosition, _mainCameraClosePosition, _lerpTimePassed / _lerpDuration);

                    _mainCamera.transform.localRotation =
                        Quaternion.Lerp(_mainCameraFarRotation, _mainCameraCloseRotation, _lerpTimePassed / _lerpDuration);

                    _lerpTimePassed += Time.deltaTime;
                }
                else
                {
                    _mainCamera.transform.localPosition = _mainCameraClosePosition;
                    _mainCamera.transform.localRotation = _mainCameraCloseRotation;
                }
            }
        }

        private void getCameraPosition()
        {   //for debug purposes only
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log(_mainCamera.transform.position);
                Debug.Log(_mainCamera.transform.rotation.eulerAngles);
            }
        }
    }
}