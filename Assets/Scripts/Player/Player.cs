
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private float _canFire = -1;
    private float _canFireUnscaled = -1;

    private GameObject _projectile;

    private Transform _target;

    private WaitForSeconds _regDuration;
    private WaitForSeconds _increaseDuration;
    private WaitForSeconds _regRechargeRate;
    private WaitForSeconds _decreasedRechargeRate;

    [HideInInspector] public bool _canSlowTime = true;

    [Header("Health And Weapon Level")]
    [SerializeField] private float _health;
    public int _weaponLevel = 1;

    [Header("Animator And Box Collider")]
    [SerializeField] private Animator _anim;
    [SerializeField] private BoxCollider _boxCollider;

    [Header("Camera Shake")]
    [SerializeField] private CameraShake _camShake;

    [Header("Speeds")]
    [SerializeField] private float _speed;
    [SerializeField] private float _slerpSpeed;
    [SerializeField] private float _firerate;

    [Header("Slow Down Time Values")]
    [SerializeField] private float _defaultDuration;
    [SerializeField] private float _increasedDuration;
    [SerializeField] private float _defaultRechargeDuration;
    [SerializeField] private float _decreasedRechargeDuration;

    [Header("Targets")]
    [SerializeField] private Transform _middleTarget;
    [SerializeField] private Transform _upTarget;
    [SerializeField] private Transform _downTarget;

    [Header("Projectiles")]
    [SerializeField] private PlayerWeapon[] _playerWeapons;

    [Header("Shield And Explosion Prefab")]
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _largeExplosion;

    [Header("Rotor")]
    [SerializeField] private Rotor _rotor;

    public float Health { get; set; }

    private void Start()
    {
        UIManager.Instance.DisplayCurrentWeapon(_weaponLevel, _playerWeapons[_weaponLevel - 1].name);
        Health = _health;
        UIManager.Instance.DisplayHealth(Health);
        _regDuration = new WaitForSeconds(_defaultDuration);
        _increaseDuration = new WaitForSeconds(_increasedDuration);
        _regRechargeRate = new WaitForSeconds(_defaultRechargeDuration);
        _decreasedRechargeRate = new WaitForSeconds(_decreasedRechargeDuration);
    }
    public void Movement(Vector2 direction)
    {
        transform.Translate(direction * _speed * Time.unscaledDeltaTime);

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
        _projectile = _playerWeapons[_weaponLevel - 1].projectile;
        if(_canFire < Time.time && _weaponLevel <= 4)
        {
            Instantiate(_projectile, transform.position + new Vector3(3f,-0.5f,0), Quaternion.identity);
            _canFire = Time.time + _firerate;
            _canFireUnscaled = Time.unscaledTime + _firerate;
        }
        else if(_canFireUnscaled < Time.unscaledTime && _weaponLevel >= 5)
        {
            Instantiate(_projectile, transform.position + new Vector3(3f, -0.5f, 0), Quaternion.identity);
            _canFire = Time.time + _firerate;
            _canFireUnscaled = Time.unscaledTime + _firerate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Powerup")
        {
            Destroy(other.gameObject);
            if (_weaponLevel < _playerWeapons.Length)
            {
                _weaponLevel++;
                Health++;
            }
            _anim.SetTrigger("Powerup");
            UIManager.Instance.DisplayCurrentWeapon(_weaponLevel, _playerWeapons[_weaponLevel - 1].name);
            UIManager.Instance.DisplayHealth(Health);
        }
    }

    public void SlowDownTime()
    {
        if (_canSlowTime == true && _weaponLevel >= 2)
            StartCoroutine(SlowTimeRoutine());
    }

    IEnumerator SlowTimeRoutine()
    {
        _canSlowTime = false;

        if (_weaponLevel >= 6)
        {
            _boxCollider.enabled = false;
            _shield.SetActive(true);
        }

        Time.timeScale = 0.3f;
        if (_weaponLevel == 2)
            yield return _regDuration;
        else
            yield return _increaseDuration;
        Time.timeScale = 1;
        _boxCollider.enabled = true;
        _shield.SetActive(false);
        StartCoroutine(RechargeTimeAbilityRoutine());
    }

    IEnumerator RechargeTimeAbilityRoutine()
    {
        if (_weaponLevel <= 3)
            yield return _regRechargeRate;
        else
            yield return _decreasedRechargeRate;
        _canSlowTime = true;
    }

    public void Damage(float damageAmount)
    {
        Health -= damageAmount;
        _weaponLevel--;
        _camShake.SetupCameraShake(0.4f, 0.4f);
        if(Health < 1)
        {
            Instantiate(_largeExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        UIManager.Instance.DisplayCurrentWeapon(_weaponLevel, _playerWeapons[_weaponLevel - 1].name);
        UIManager.Instance.DisplayHealth(Health);
    }
}
