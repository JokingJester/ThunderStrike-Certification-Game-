using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private AudioClip _bossMusic;
    [SerializeField] private int _waveNumber = 1;
    [SerializeField] private Player _player;

    private WaitForSeconds _startSpawnSeconds;
    private WaitForSeconds _endWaveSeconds;

    private bool _spawnEnemies;
    private int _waveIndex;
    public Wave[] _waves;
    // Start is called before the first frame update
    void Start()
    {
        _startSpawnSeconds = new WaitForSeconds(7);
        _endWaveSeconds = new WaitForSeconds(1.5f);
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        UIManager.Instance.DisplayCurrentWave(_waveNumber);
        _spawnEnemies = true;
        yield return _startSpawnSeconds;
        while (_spawnEnemies == true)
        {

            if(_waves[_waveNumber - 1].waveData[_waveIndex].spawnWithNoEnemies == false)
            {
                yield return new WaitForSeconds(_waves[_waveNumber - 1].waveData[_waveIndex].spawnRate);
                GameObject enemy = Instantiate(_waves[_waveNumber - 1].waveData[_waveIndex].enemyPrefab);

                if (_waves[_waveNumber - 1].waveData[_waveIndex].bossFight == true)
                {
                    AudioManager.Instance.ChangeClip(_bossMusic);
                }

                if (_waves[_waveNumber - 1].waveData[_waveIndex].hasPowerup == true || _waves[_waveNumber - 1].waveData[_waveIndex].playSpecificPattern == true)
                {
                    Enemy enemyScript = enemy.GetComponent<Enemy>();
                    if (enemyScript != null)
                    {
                        if(_waves[_waveNumber - 1].waveData[_waveIndex].hasPowerup == true)
                            enemyScript.hasPowerup = true;

                        if (_waves[_waveNumber - 1].waveData[_waveIndex].playSpecificPattern == true)
                            enemyScript.ChooseSelectedPattern(_waves[_waveNumber - 1].waveData[_waveIndex].specificPattern);
                        else
                            enemyScript.ChooseRandomPattern();
                    }
                }

            }
            else if(_waves[_waveNumber - 1].waveData[_waveIndex].spawnWithNoEnemies == true)
            {
                while(NoEnemiesOnScreen() == false)
                {
                    yield return null;
                    if(NoEnemiesOnScreen() == true)
                    {
                        yield return new WaitForSeconds(_waves[_waveNumber - 1].waveData[_waveIndex].spawnRate);
                        GameObject enemy = Instantiate(_waves[_waveNumber - 1].waveData[_waveIndex].enemyPrefab);
                        
                        if (_waves[_waveNumber - 1].waveData[_waveIndex].hasPowerup == true || _waves[_waveNumber - 1].waveData[_waveIndex].playSpecificPattern == true)
                        {
                            Enemy enemyScript = enemy.GetComponent<Enemy>();
                            if (enemyScript != null)
                            {
                                if (_waves[_waveNumber - 1].waveData[_waveIndex].hasPowerup == true)
                                    enemyScript.hasPowerup = true;

                                if (_waves[_waveNumber - 1].waveData[_waveIndex].playSpecificPattern == true)
                                    enemyScript.ChooseSelectedPattern(_waves[_waveNumber - 1].waveData[_waveIndex].specificPattern);
                                else
                                    enemyScript.ChooseRandomPattern();
                            }
                        }
                        break;
                    }
                }
            }

            if (_waveIndex == _waves[_waveNumber - 1].waveData.Length - 1)
            {
                _spawnEnemies = false;
            }
            else
            {
                _waveIndex++;
            }
        }

        while (NoEnemiesOnScreen() == false)
        {
            yield return null;
            if (NoEnemiesOnScreen() == true)
            {
                if (_waveNumber != _waves.Length)
                {
                    _waveNumber++;
                    _waveIndex = 0;
                    yield return _endWaveSeconds;
                    AudioManager.Instance.ChangeToRegularMusic();
                    StartCoroutine(SpawnRoutine());
                }
                else
                {
                    yield return _endWaveSeconds;
                    _player._canPause = false;
                    AudioManager.Instance.PlayVictoryMusic();
                    UIManager.Instance.ShowVictoryCanvas();
                }
            }
        }
    }


    private bool NoEnemiesOnScreen()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (allEnemies.Length >= 1)
            return false;

        return true;
    }
}
