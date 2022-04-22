using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private float currentHealth;
    public float CurrentHealth {
       get => currentHealth;
        set
        {
            currentHealth = value;
        }
    }
    [SerializeField] private float maxHealth;
    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
        }
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

 
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (damage <= 0)
        {
            Destroy();
        }
    }
    public virtual void Heal(float healValue)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + healValue, 0, maxHealth);
        }
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
