using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    [SerializeField] private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);

        if (transform.position.y <= -20)
            Destroy(this.gameObject);
    }
}
