using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossBomb : Projectile
{
    public enum DirectionToMove { Left, Right, Up, Down}
    public DirectionToMove _direction;
    [SerializeField] private float _speed;

    // Update is called once per frame
    public virtual void Update()
    {
        switch (_direction)
        {
            case DirectionToMove.Left:
                transform.Translate(Vector2.left * _speed * Time.deltaTime);
                break;
            case DirectionToMove.Right:
                transform.Translate(Vector2.right * _speed * Time.deltaTime);
                break;
            case DirectionToMove.Up:
                transform.Translate(Vector2.up * _speed * Time.deltaTime);
                break;
            case DirectionToMove.Down:
                transform.Translate(Vector2.down * _speed * Time.deltaTime);
                break;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _damagePlayer == true)
        {
            IDamageable damage = other.GetComponent<IDamageable>();
            if (damage != null)
                damage.Damage(_damageAmount);
            GameObject smallExplosion = PoolManager.Instance.RequestPrefab(transform.position, 6);
            Destroy(this.gameObject);
        }
    }
}
