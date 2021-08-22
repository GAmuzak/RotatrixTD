using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            ReduceHealth();
        }
    }

    private void ReduceHealth()
    {
        enemyHealth--;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
