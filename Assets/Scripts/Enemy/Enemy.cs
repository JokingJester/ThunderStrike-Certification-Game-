using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Enemy : MonoBehaviour, IDamageable
{
    private double _duration;
    [Header("Health Settngs")]
    [SerializeField] private float _health;
    [SerializeField] private float _explosionScale;
    [SerializeField] private GameObject _bigExplosion;
    [SerializeField] private GameObject _powerup;

    [Header("Flight Pattern Settings")]
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private PlayableAsset[] _assets;
    [SerializeField] private Quaternion _rotation;

    [HideInInspector] public bool hasPowerup;
    [HideInInspector] public bool hitByChainFireball;
    public float Health { get; set; }

    void Start()
    {
        if(hasPowerup == true)
        {
            MeshRenderer mesh = GetComponent<MeshRenderer>();
            if (mesh != null)
                mesh.material.color = Color.blue;
        }
        Health = _health;
    }

    public void ChooseRandomPattern()
    {
        int randomNumb = Random.Range(0, _assets.Length - 1);
        _director.playableAsset = _assets[randomNumb];
        _duration = Time.time + _director.playableAsset.duration;
        _director.Play();
    }

    public void ChooseSelectedPattern(int number)
    {
        _director.playableAsset = _assets[number];
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

        if (Health < 1)
        {
            GameObject explosion = Instantiate(_bigExplosion, transform.position, Quaternion.identity);
            if(_explosionScale != 0)
                explosion.transform.localScale = new Vector3(_explosionScale, _explosionScale, _explosionScale);

            if (hasPowerup == true)
                Instantiate(_powerup, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            IDamageable idamage = other.GetComponent<IDamageable>();
            if (idamage != null)
                idamage.Damage(1);
        }
    }

    public void CallCoroutine()
    {
        StartCoroutine(SetBoolBackToFalseRoutine());
    }

    public IEnumerator SetBoolBackToFalseRoutine()
    {
        hitByChainFireball = true;
        yield return new WaitForSeconds(0.2f);
        hitByChainFireball = false;
    }
}
