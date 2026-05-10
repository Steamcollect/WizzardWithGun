using System;
using MVsToolkit.Attributes;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, ReadOnly] int maxHealth;
    [SerializeField, ReadOnly] int currentHealth;

    protected EntityStatistics statistics;

    public Action OnDeath;

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
        OnDeath?.Invoke();
    }
}