using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public bool _canDamage = true;
    [SerializeField] protected GameObject _smallExplosion;
    [SerializeField] protected bool _damagePlayer;
    [SerializeField] protected float _damageAmount;

    [Header("Audio")]
    [SerializeField] protected AudioClip _sound;
    [SerializeField] protected AudioClip _hitSound;
    [SerializeField] protected float _volume;
    [SerializeField] protected float _hitSoundVolume;

    public virtual void Start()
    {
        AudioManager.Instance.PlayOneShot(_sound, _volume);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && _damagePlayer == false)
        {
            AudioManager.Instance.PlayOneShot(_hitSound, _hitSoundVolume);
            if(_smallExplosion != null)
                Instantiate(_smallExplosion, transform.position, Quaternion.identity);
            if (_canDamage == false)
                return;
            IDamageable damage = other.GetComponent<IDamageable>();
            if (damage != null)
                damage.Damage(_damageAmount);

            Destroy(this.gameObject);
        }

        if(other.tag == "Player" && _damagePlayer == true)
        {
            IDamageable damage = other.GetComponent<IDamageable>();
            if (damage != null)
                damage.Damage(_damageAmount);
            Instantiate(_smallExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
