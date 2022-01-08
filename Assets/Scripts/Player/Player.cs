using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Movement(Vector2 direction)
    {
        transform.Translate(direction * _speed * Time.deltaTime);
    }
    //create movement method with vector 2 parameter direction
    //transform translate(direction * speed * time.deltatime
}
