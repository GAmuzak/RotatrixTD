using TMPro;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthTextBox;
    [Range(50, 150)][SerializeField] private int startingHealth;

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
        if (health <= 0) return;
        health--;
        healthTextBox.text = health.ToString();
    }
}
