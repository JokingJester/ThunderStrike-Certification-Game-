using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    [SerializeField] protected float _speed;
    private Player _player;
    // Start is called before the first frame update

    public override void Start()
    {
        base.Start();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            _player = player.GetComponent<Player>();
    }
    // Update is called once per frame
    public virtual void Update()
    {
        if (transform.position.x >= 32)
            Destroy(this.gameObject);

        if (_damagePlayer == false)
        {
            if (_player != null)
            {
                if (_player._weaponLevel >= 5)
                {
                    transform.Translate(Vector2.right * _speed * Time.unscaledDeltaTime);
                    return;
                }
            }
        }
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }
}
