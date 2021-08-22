using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private GameObject enemyType;
    [SerializeField] private Transform finish;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private float startOffset;
    [SerializeField] private float spawnRate;
    
    private bool canSpawn=true;
    
    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy),startOffset,spawnRate);
    }

    private void SpawnEnemy()
    {
        if (!canSpawn) return;
        GameObject enemy=Instantiate(enemyType, transform.position, Quaternion.identity,enemyContainer);
        NavMeshAgent enemyNavMesh = enemy.GetComponent<NavMeshAgent>();
        enemyNavMesh.SetDestination(finish.position);
    }
}
