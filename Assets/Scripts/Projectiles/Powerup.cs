using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private WaitForSeconds _seconds;
    [SerializeField] private float _speed = 8;

    void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);

        if (transform.position.x <= -34)
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.UpgradeWeaponAndHealth();
                this.gameObject.SetActive(false);
            }
        }
    }
}
