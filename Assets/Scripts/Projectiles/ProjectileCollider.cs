using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player Weapon")
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if(projectile != null)
            {
                projectile._canDamage = false;
            }
        }
    }
}
