using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Enemy : MonoBehaviour, IDamageable
{
    private double _duration;
    private string player = "Player";
    [Header("Health Settngs")]
    [SerializeField] private float _health;

    [Header("Flight Pattern Settings")]
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private PlayableAsset[] _assets;
    [SerializeField] private Quaternion _rotation;

    [Header("Audio")]
    [SerializeField] private AudioClip _hitSound;

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
            else
            {
                mesh = GetComponentInChildren<MeshRenderer>();
                if(mesh != null)
                {
                    mesh.material.color = Color.blue;
                }
            }

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

    public void ShootProjectile(int id)
    {
        GameObject prefab = PoolManager.Instance.RequestPrefab(transform.position, id);
        prefab.transform.rotation = _rotation;
    }

    public void Damage(float damageAmount)
    {
        Health -= damageAmount;

        if (Health < 1)
        {
            UIManager.Instance.AddScore(40);
            GameObject explosion = PoolManager.Instance.RequestPrefab(transform.position, 8);
            //play explosion sound

            //if multi explosion = true
            //spawn multi explosion

            if (hasPowerup == true)
            {
                GameObject powerup = PoolManager.Instance.RequestPrefab(transform.position, 7);
            }
            Destroy(this.gameObject);
        }
        if (_hitSound != null)
            AudioSource.PlayClipAtPoint(_hitSound, transform.position, MainMenu.audioVolume);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == player)
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
