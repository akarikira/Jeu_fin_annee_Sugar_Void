using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 30;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Ennemi touché! Santé restante: {health}");
        
        if(health <= 0)
            Destroy(gameObject);
    }
}