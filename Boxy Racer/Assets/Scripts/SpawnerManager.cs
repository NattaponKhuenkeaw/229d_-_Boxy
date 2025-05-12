using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class SpawnerManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public int maxSpawnPerWave = 3;
    public float spawnInterval = 5f;
    public float initialEnemySpeed = 10f;
    public float speedIncreasePerWave = 2f;

    [Header("Box Settings")]
    public GameObject boxPrefab;
    public float boxSpawnInterval = 2f;
    public int boxesPerWave = 10;

    [Header("Wave Settings")]
    public float waveDelay = 3f;
    public float speedOfSpawn = 0.5f;

    [Header("References")]
    public PlayerController player;
    public GameObject roadPrefab;
    public TextMeshProUGUI scoreText;

    private HashSet<Transform> occupiedSpawnPoints = new HashSet<Transform>();
    private int currentWave = 0;
    private int collectedBoxes = 0;
    private float currentEnemySpeed;
    private bool isWaitingForWave = false;

    void Start()
    {
        currentEnemySpeed = initialEnemySpeed;
        InvokeRepeating(nameof(SpawnEnemies), 1f, spawnInterval);
        InvokeRepeating(nameof(SpawnBox), 1f, boxSpawnInterval);
    }

    void Update()
    {
        if (collectedBoxes == boxesPerWave && !isWaitingForWave)
        {
            StartCoroutine(StartNewWaveWithDelay());
            collectedBoxes = 0;
        }

        if (int.TryParse(scoreText.text, out int score) && score == 50)
        {
            boxesPerWave = 10 + (score / 50) * 5;
        }
    }

    private void SpawnEnemies()
    {
        occupiedSpawnPoints.Clear();

        int spawnCount = Mathf.Min(Random.Range(1, maxSpawnPerWave + 1), spawnPoints.Length);
        List<Transform> availableSpawns = new List<Transform>(spawnPoints);

        for (int i = 0; i < spawnCount; i++)
        {
            if (availableSpawns.Count == 0) break;

            int randomIndex = Random.Range(0, availableSpawns.Count);
            Transform spawnPoint = availableSpawns[randomIndex];
            availableSpawns.RemoveAt(randomIndex);

            occupiedSpawnPoints.Add(spawnPoint);

            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject selectedEnemyPrefab = enemyPrefabs[randomEnemyIndex];
            GameObject enemy = Instantiate(selectedEnemyPrefab, spawnPoint.position, selectedEnemyPrefab.transform.rotation);

            MoveBack movement = enemy.GetComponent<MoveBack>();
            if (movement != null)
            {
                movement.speed = currentEnemySpeed;
            }
        }
    }

    private void SpawnBox()
    {
        if (boxPrefab == null || spawnPoints.Length == 0) return;

        List<Transform> availableSpawns = new List<Transform>();
        foreach (Transform sp in spawnPoints)
        {
            if (!occupiedSpawnPoints.Contains(sp))
                availableSpawns.Add(sp);
        }

        Transform selectedSpawn = availableSpawns.Count > 0
            ? availableSpawns[Random.Range(0, availableSpawns.Count)]
            : spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject box = Instantiate(boxPrefab, selectedSpawn.position, Quaternion.identity);
        collectedBoxes++;

        Box boxScript = box.GetComponent<Box>();
        if (boxScript != null)
        {
            boxScript.SetSpeed(currentEnemySpeed);
        }
    }

    private IEnumerator StartNewWaveWithDelay()
    {
        isWaitingForWave = true;
        Debug.Log($"Wave {currentWave + 1} will start in {waveDelay} seconds...");

        MoveBack roadScript = roadPrefab.GetComponent<MoveBack>();
        if (roadScript != null)
        {
            roadScript.speed = currentEnemySpeed;
        }

        CancelInvoke(nameof(SpawnEnemies));
        CancelInvoke(nameof(SpawnBox));

        yield return new WaitForSeconds(waveDelay);

        currentWave++;
        currentEnemySpeed += speedIncreasePerWave;
        Debug.Log($"Wave {currentWave} started! Enemy speed: {currentEnemySpeed}");

        if (player != null)
        {
            player.IncreaseSpeedByWave(currentWave);
        }

        spawnInterval = Mathf.Max(0.75f, spawnInterval - speedOfSpawn);
        InvokeRepeating(nameof(SpawnEnemies), 0f, spawnInterval);
        InvokeRepeating(nameof(SpawnBox), 0f, spawnInterval);

        isWaitingForWave = false;
    }
}
