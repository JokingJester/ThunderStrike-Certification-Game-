using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giftbox : FinalBossBomb
{
    [SerializeField] private GameObject _bombs;
    private WaitForSeconds _seconds;

    private void Awake()
    {
        _seconds = new WaitForSeconds(1.5f);
    }

    private void OnEnable()
    {
        int randomNumber = Random.Range(1, 3);
        if (randomNumber == 1)
            _direction = DirectionToMove.Up;
        else if (randomNumber == 2)
            _direction = DirectionToMove.Left;
        else if (randomNumber == 3)
            _direction = DirectionToMove.Down;
        StartCoroutine(SpawnGiftRoutine());
    }

    IEnumerator SpawnGiftRoutine()
    {
        yield return _seconds;
        Instantiate(_bombs, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
