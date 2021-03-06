using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : HealthComponent
{
    [SerializeField] private HealthPickup pickupToSpawn;
    [SerializeField] private Collider colliderComponent;
    [SerializeField] private Transform pickupTarget;
    [SerializeField] private Rigidbody rb;

    protected override void Start()
    {
        base.Start();
        colliderComponent = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void TargetTakeDamage(float damage, Transform targetFocus)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            pickupTarget = targetFocus;
            Destroy();
        }
    }

    public override void Destroy()
    {
        StartCoroutine(DestroyDelay());
        base.Destroy();
    }

    IEnumerator DestroyDelay()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        colliderComponent.enabled = false;
        pickupToSpawn = (HealthPickup)Instantiate(pickupToSpawn, transform.position, Quaternion.identity);
        pickupToSpawn.FindTarget(pickupTarget);
        yield return new WaitForSeconds(2.5f);
    }
}
