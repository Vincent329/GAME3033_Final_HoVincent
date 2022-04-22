using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healValue;
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthComponent playerHealth = other.GetComponent<PlayerHealthComponent>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healValue);
            Destroy(gameObject);
        }
    }
}
