using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private GameObject enemyType;
    [SerializeField] private Transform finish;
    [SerializeField] private Transform enemyContainer;
    [SerializeField] private bool canSpawn;
    

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy),0f,2f);
    }

    private void SpawnEnemy()
    {
        if (!canSpawn) return;
        GameObject enemy=Instantiate(enemyType, transform.position, Quaternion.identity,enemyContainer);
        NavMeshAgent enemyNavMesh = enemy.GetComponent<NavMeshAgent>();
        enemyNavMesh.SetDestination(finish.position);
    }
}
