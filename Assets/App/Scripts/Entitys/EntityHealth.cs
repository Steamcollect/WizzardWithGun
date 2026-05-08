using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [Header("Settings")]
    int maxHealth;
    int currentHealth;

    public Action onDeath;

    protected EntityStatistics statistics;

    public EntityHealth Initialize(EntityStatistics statistics)
    {
        maxHealth = statistics.Health;
        currentHealth = maxHealth;

        this.statistics = statistics;
        return this;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        onDeath?.Invoke();
    }
}