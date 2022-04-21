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

    private string enemy = "Enemy";


    private void OnEnable()
    {
        AudioManager.Instance.PlayOneShot(_sound, _volume);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == enemy && _damagePlayer == false)
        {
            AudioManager.Instance.PlayOneShot(_hitSound, _hitSoundVolume);
            //request pool manager 6 
            GameObject smallExplosion = PoolManager.Instance.RequestPrefab(transform.position, 6);
            if (_canDamage == false)
                return;
            IDamageable damage = other.GetComponent<IDamageable>();
            if (damage != null)
                damage.Damage(_damageAmount);

            if (this.transform.parent.transform.parent.transform.parent == null)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(false);
                if (transform.parent.GetChild(0).gameObject.activeInHierarchy == false && transform.parent.GetChild(1).gameObject.activeInHierarchy == false)
                {
                    transform.parent.gameObject.SetActive(false);
                }
            }
        }

        if(other.tag == "Player" && _damagePlayer == true)
        {
            IDamageable damage = other.GetComponent<IDamageable>();
            if (damage != null)
                damage.Damage(_damageAmount);
            GameObject smallExplosion = PoolManager.Instance.RequestPrefab(transform.position, 6);

            if (this.transform.parent.transform.parent.transform.parent == null)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(false);
                if (transform.parent.GetChild(0).gameObject.activeInHierarchy == false && transform.parent.GetChild(1).gameObject.activeInHierarchy == false)
                {
                    transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }
}
