using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    [SerializeField] private float _speed;
    private Player _player;
    // Start is called before the first frame update

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            _player = player.GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if(_player != null)
        {
            if(_player._canSlowTime == false && _player._weaponLevel >= 4)
            {
                transform.Translate(Vector2.right * _speed * Time.unscaledDeltaTime);
                return;
            }
        }

        transform.Translate(Vector2.right * _speed * Time.deltaTime);
        if (transform.position.x >= 32)
            Destroy(this.gameObject);
    }
}
