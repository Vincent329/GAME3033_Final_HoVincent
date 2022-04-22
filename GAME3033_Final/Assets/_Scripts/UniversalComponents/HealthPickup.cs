using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healValue;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool targetFound;
    [SerializeField] private Transform target;
    [SerializeField] private float attractSpeed;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        targetFound = false;
    }

    private void Update()
    {
        if (targetFound)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, attractSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthComponent playerHealth = other.GetComponent<PlayerHealthComponent>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healValue);
            Destroy(gameObject);
        }
    }

    public void FindTarget(Transform attractOrigin)
    {
        target = attractOrigin;
        targetFound = true;
    }
}
