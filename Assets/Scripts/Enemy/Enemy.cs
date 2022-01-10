using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Enemy : MonoBehaviour, IDamageable
{
    private double _duration;

    [SerializeField] private float _health;
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private PlayableAsset[] _assets;
    [SerializeField] private Quaternion _rotation;

    public float Health { get; set; }

    void Start()
    {
        Health = _health;
        ChooseRandomPattern();
    }

    private void ChooseRandomPattern()
    {
        int randomNumb = Random.Range(0, _assets.Length - 1);
        _director.playableAsset = _assets[randomNumb];
        _duration = Time.time + _director.playableAsset.duration;
        _director.Play();
    }

    void Update()
    {
        if(Time.time > _duration)
        {
            ChooseRandomPattern();
        }
    }

    public void ShootProjectile(GameObject prefab)
    {
        Instantiate(prefab, transform.position, _rotation);
    }

    public void Damage(float damageAmount)
    {
        Health -= damageAmount;

        if(Health < 1)
        {
            //spawn big explosion
            //destory
            Destroy(this.gameObject);
        }
    }
}
