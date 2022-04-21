using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableExplosion : MonoBehaviour
{
    [SerializeField] private float seconds;
    private WaitForSeconds _activeSeconds;

    private void Awake()
    {
        _activeSeconds = new WaitForSeconds(seconds);
    }
    private void OnEnable()
    {
        StartCoroutine(DisableRoutine());
    }

    IEnumerator DisableRoutine()
    {
        yield return _activeSeconds;
        this.gameObject.SetActive(false);
    }
}
