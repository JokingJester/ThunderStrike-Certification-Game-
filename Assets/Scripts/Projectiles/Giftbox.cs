using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giftbox : FinalBossBomb
{
    [SerializeField] private GameObject _bombs;
    public override void Start()
    {
        base.Start();
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
        yield return new WaitForSeconds(1.5f);
        Instantiate(_bombs, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
