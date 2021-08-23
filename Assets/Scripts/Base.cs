using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthTextBox;
    [Range(50, 150)][SerializeField] private int startingHealth;
    [SerializeField] private GameManager gameManager;

    private int health = 0;
    private void Start()
    {
        healthTextBox.text = startingHealth.ToString();
        health = startingHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            ReduceHealth();
        }
    }

    private void ReduceHealth()
    {
        if (health <= 0)
        {
            gameManager.EndGame();
            return;
        }
        health--;
        healthTextBox.text = health.ToString();
    }
}
