using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected GameObject _smallExplosion;
    [SerializeField] protected bool _damagePlayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && _damagePlayer == false)
        {
            Instantiate(_smallExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if(other.tag == "Player" && _damagePlayer == true)
        {
            Instantiate(_smallExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
