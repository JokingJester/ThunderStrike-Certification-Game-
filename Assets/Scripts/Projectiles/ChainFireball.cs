using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainFireball : Fireball
{
    [SerializeField] private AudioClip _richochetSound;
    private int _enemiesHit;
    private Transform _enemyTarget;
    public override void Update()
    {
        if (_enemiesHit >= 2)
        {
            ResetChainLaser();
        }

        if(_enemyTarget == null)
            base.Update();
        else
        {
            Vector3 direction = _enemyTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
            transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.position, _speed * Time.unscaledDeltaTime);
        }
    }

    public override void InactiveWhenOutOfBounds()
    {
        if (transform.position.x >= 32 || transform.position.y >= 26 || transform.position.y <= -26 || transform.position.x <= -32)
        {
            if (this.transform.parent.transform.parent.transform.parent == null)
            {
                ResetChainLaser();
            }
            else
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && _damagePlayer == false)
        {
            IDamageable damage = other.GetComponent<IDamageable>();
            if (damage != null)
                damage.Damage(_damageAmount);
            GameObject smallExplosion = PoolManager.Instance.RequestPrefab(transform.position, 6);

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
                enemy.CallCoroutine();
            _enemiesHit++;
            StartCoroutine(FindTargetRoutine());
        }
    }

    private IEnumerator FindTargetRoutine()
    {
        yield return null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(allEnemies.Length >=2)
            AudioManager.Instance.PlayOneShot(_richochetSound, _volume + 1);
        if (allEnemies.Length >= 1)
        {
            float minimumDistance = Mathf.Infinity;
            Transform target = null;
            foreach(var enemy in allEnemies)
            {
                float distance = Vector3.Distance(enemy.transform.position, this.transform.position);
                if(distance < minimumDistance)
                {
                    Enemy enemyScript = enemy.GetComponent<Enemy>();
                    if(enemyScript != null)
                    {
                        if(enemyScript.hitByChainFireball == false)
                        {
                            minimumDistance = distance;
                            target = enemy.transform;
                        }
                    }
                }
            }
            _enemyTarget = target;
        }

        if (_enemyTarget == null)
            ResetChainLaser();
    }

    public void ResetChainLaser()
    {
        _enemiesHit = 0;
        _enemyTarget = null;
        transform.eulerAngles = Vector3.zero;
        this.gameObject.SetActive(false);
    }
}
