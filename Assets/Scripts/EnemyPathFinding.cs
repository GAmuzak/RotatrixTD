using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private GameObject enemyType;
    [SerializeField] private Transform finish;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private float startOffset;
    [SerializeField] private float spawnRate;
    [SerializeField] private int countPerWave;
    [SerializeField] private float waveCoolDown;

    private bool canSpawn=true;
    private int currentWaveCount = 0;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy),startOffset,spawnRate);
    }

    private void SpawnEnemy()
    {
        if (!canSpawn) return;
        if (currentWaveCount >= countPerWave)
        {
            currentWaveCount = 0;
            canSpawn = false;
            StartCoroutine(SpawnerCooldown());
            return;
        }
        GameObject enemy=Instantiate(enemyType, transform.position, Quaternion.identity,enemyContainer);
        NavMeshAgent enemyNavMesh = enemy.GetComponent<NavMeshAgent>();
        enemyNavMesh.SetDestination(finish.position);
        currentWaveCount++;
    }

    private IEnumerator SpawnerCooldown()
    {
        yield return new WaitForSeconds(waveCoolDown);
        canSpawn = true;
    }
}
