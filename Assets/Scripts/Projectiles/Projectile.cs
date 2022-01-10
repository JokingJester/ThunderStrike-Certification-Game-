using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected GameObject _smallExplosion;
    [SerializeField] protected bool _damagePlayer;
    [SerializeField] protected float _damageAmount;

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && _damagePlayer == false)
        {
            IDamageable damage = other.GetComponent<IDamageable>();
            if (damage != null)
                damage.Damage(_damageAmount);
            Instantiate(_smallExplosion, transform.position, Quaternion.identity);
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
