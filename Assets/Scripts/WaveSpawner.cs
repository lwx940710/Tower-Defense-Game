using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5.5f;
    private float countdown;
    private int waveNo = 1;
    private bool curWaveSpawned = true;

    public Text countdownText;
    public Text waveNoText;

    private void Start()
    {
        countdown = timeBetweenWaves;
        countdownText.text = Mathf.Round(countdown).ToString();
        waveNoText.text = waveNo.ToString();
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveNo; ++i)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.3f);
        }
        countdown = timeBetweenWaves;
        waveNo++;
        curWaveSpawned = true;
    }

    private void Update()
    {
        if (curWaveSpawned)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f)
            {
                curWaveSpawned = false;
                StartCoroutine(SpawnWave());
            }

            countdownText.text = Mathf.Round(countdown).ToString();
            waveNoText.text = waveNo.ToString();
        }
    }
}
