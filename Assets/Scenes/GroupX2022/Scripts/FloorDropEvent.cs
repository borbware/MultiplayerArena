using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GroupX
{
    public class FloorDropEvent : MonoBehaviour
    {
        [SerializeField] private AudioSource _dropWarningAudio;
        [SerializeField] private AudioSource _floorDropAudio;
        [SerializeField] private AudioSource _musicAudio;
        [SerializeField] private AudioClip _musicSpedUpClip;
        [SerializeField] private List<GameObject> _outerHexes;

        private double _timeTillDrop;
        private double _totalStageTime;
        private readonly float _dropAfterPercent = 0.6f;  //the stage drop after this amount of the total stage time has elapsed
        private bool _eventTriggered = false;
        private bool _warningSounded = false;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "Readonly would be misleading")]
        private List<MeshRenderer> _meshRenderers = new();

        private Camera _mainCamera;
        private float _lerpTimePassed = 0f;
        private readonly float _lerpDuration = 3f;

        private void Awake()
        {
            foreach (GameObject hex in _outerHexes)
                _meshRenderers.AddRange(hex.GetComponentsInChildren<MeshRenderer>());

            _mainCamera = Camera.main;
        }

        private void Start()
        {
            // In start, because StageManager.instance is set in Awake
            _totalStageTime = StageManager.instance.stageTime;
            _timeTillDrop = _totalStageTime * _dropAfterPercent;
        }

        private void Update()
        {
            WarnIfNeeded();
            TriggerEventIfNeeded();
            Debug_GetCameraPosition();
        }

        private void WarnIfNeeded()
        {
            if (_warningSounded)
                return;

            float elapsedStageTime = (float)(_totalStageTime - StageManager.instance.stageTime);
            if (elapsedStageTime >= _timeTillDrop - 2.9)
            {
                _warningSounded = true;
                StartCoroutine(PlayWarningSounds());
                foreach (var meshRenderer in _meshRenderers)
                    StartCoroutine(FlashReddish(meshRenderer));
            }

            return;

            IEnumerator PlayWarningSounds()
            {
                yield return new WaitForSeconds(0.1f);
                while (!_eventTriggered)
                {
                    _dropWarningAudio.Play();
                    yield return new WaitForSeconds(1f);
                }
            }

            IEnumerator FlashReddish(MeshRenderer meshRenderer)
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
        }

        private void TriggerEventIfNeeded()
        {
            if (_eventTriggered)
                return;

            float elapsedStageTime = (float)(_totalStageTime - StageManager.instance.stageTime);
            if (elapsedStageTime > _timeTillDrop)
            {
                _eventTriggered = true;

                RemoveOuterMoleHoles();
                DropOuterHexes();
                SpeedUpMusic();
                StartCoroutine(MoveCamera());
            }

            return;

            void RemoveOuterMoleHoles() => GetComponent<MoleSpawner>().listOfHoles.RemoveLast(6);

            void DropOuterHexes()
            {
                _floorDropAudio.Play();
                foreach (GameObject hex in _outerHexes)
                {
                    hex.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    hex.GetComponent<Rigidbody>().useGravity = true;
                }
            }

            void SpeedUpMusic()
            {
                _musicAudio.Pause();
                float correspondingSpedUpSamples = Util.Remap(0, _musicAudio.clip.samples, 0, _musicSpedUpClip.samples, _musicAudio.timeSamples);
                _musicAudio.clip = _musicSpedUpClip;
                _musicAudio.timeSamples = Mathf.RoundToInt(correspondingSpedUpSamples);
                _musicAudio.Play();
            }

            IEnumerator MoveCamera()
            {
                //feel free to change these 4 vectors and experiment with them
                Vector3 _mainCameraFarPosition = new(-339.5f, -200f, 41f);
                Quaternion _mainCameraFarRotation = Quaternion.Euler(66.5f, 0f, 0f);
                Vector3 _mainCameraClosePosition = new(-340f, -207f, 43f);
                Quaternion _mainCameraCloseRotation = Quaternion.Euler(45f, 0f, 0f);

                while (_mainCamera.transform.position != _mainCameraClosePosition)
                {
                    if (_lerpTimePassed < _lerpDuration)
                    {

                        _mainCamera.transform.SetLocalPositionAndRotation(
                            Vector3.Lerp(_mainCameraFarPosition, _mainCameraClosePosition, _lerpTimePassed / _lerpDuration),
                            Quaternion.Lerp(_mainCameraFarRotation, _mainCameraCloseRotation, _lerpTimePassed / _lerpDuration));

                        _lerpTimePassed += Time.deltaTime;
                    }
                    else
                    {
                        _mainCamera.transform.SetLocalPositionAndRotation(
                            _mainCameraClosePosition,
                            _mainCameraCloseRotation);
                    }
                    yield return null;
                }
            }
        }

        private void Debug_GetCameraPosition()
        {   //for debug purposes only
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log(_mainCamera.transform.position);
                Debug.Log(_mainCamera.transform.rotation.eulerAngles);
            }
        }
    }
}