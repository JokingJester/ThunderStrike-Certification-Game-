using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int _weaponLevel = 1;
    private float _canFire = -1;
    private GameObject _projectile;
    private Transform _target;

    [Header("Speeds")]
    [SerializeField] private float _speed;
    [SerializeField] private float _slerpSpeed;
    [SerializeField] private float _firerate;

    [Header("Targets")]
    [SerializeField] private Transform _middleTarget;
    [SerializeField] private Transform _upTarget;
    [SerializeField] private Transform _downTarget;

    [Header("Projectiles")]
    [SerializeField] private GameObject _singleFireballPrefab;
    [SerializeField] private GameObject _doubleFireballPrefab;
    [SerializeField] private GameObject _tripleFireballPrefab;
    [SerializeField] private GameObject _wavePrefab;
    [SerializeField] private GameObject _bigFireballPrefab;
    [SerializeField] private GameObject _chainFireballPrefab;

    [Header("Rotor")]
    [SerializeField] private Rotor _rotor;

    public void Movement(Vector2 direction)
    {
        transform.Translate(direction * _speed * Time.deltaTime);

        if (direction.y != 0)
            _target = direction.y == 1 ? _upTarget : _downTarget;
        else
            _target = _middleTarget;

        _rotor._speed = direction.x == 0 ? 900 : 1500;
        var targetDirection = _target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _slerpSpeed * Time.deltaTime);
    }

    private void Update()
    {
        ClampPlayer();
    }

    private void ClampPlayer()
    {
        Vector2 clampedPosition = transform.position;
        Quaternion clampedRotation = transform.rotation;

        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -17, 13);
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -26, 26);
        clampedRotation.y = Mathf.Clamp(clampedRotation.y, 0, 0);

        transform.position = clampedPosition;
        transform.rotation = clampedRotation;
    }

    public void Shoot()
    {
        //switch between weapon level
        switch (_weaponLevel)
        {
            case 1:
                _projectile = _singleFireballPrefab;
                break;
            case 2:
                _projectile = _doubleFireballPrefab;
                break;
            case 3:
                _projectile = _tripleFireballPrefab;
                break;
            case 4:
                _projectile = _wavePrefab;
                break;
            case 5:
                _projectile = _bigFireballPrefab;
                break;
            case 6:
                _projectile = _chainFireballPrefab;
                break;
        }
        if(Time.time > _canFire)
        {
            Instantiate(_projectile, transform.position + new Vector3(3f,-0.5f,0), Quaternion.identity);
            _canFire = Time.time + _firerate;
        }
    }
}
