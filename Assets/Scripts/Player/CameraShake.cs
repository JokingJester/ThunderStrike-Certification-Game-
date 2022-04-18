using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    [HideInInspector] public bool canShakeCamera;

    private float _duration;
    private float _shakeFrequency = 0.2f;
    private float _secondsCameraShakes = 0.6f;

    private Vector3 _originalPosOfCam;

    void Start()
    {
        _originalPosOfCam = _cameraTransform.position;
    }

    void Update()
    {
        if (canShakeCamera == true && _duration > Time.time)
            ShakeCamera();
        else if (Time.time > _duration && canShakeCamera == true)
            canShakeCamera = false;
    }

    private void ShakeCamera()
    {
        _cameraTransform.transform.position = _originalPosOfCam + Random.insideUnitSphere * _shakeFrequency;
    }

    public void SetupCameraShake(float secondsCameraShakes, float shakeFrequency)
    {
        _shakeFrequency = shakeFrequency;
        _secondsCameraShakes = secondsCameraShakes;
        _duration = Time.time + _secondsCameraShakes;
        canShakeCamera = true;
    }
}
