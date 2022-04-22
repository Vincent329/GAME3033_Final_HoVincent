using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetComponent : HealthComponent
{
    [SerializeField] private HealthPickup pickupToSpawn;
    [SerializeField] private Collider colliderComponent;
    [SerializeField] private Transform pickupTarget;
    [SerializeField] private Rigidbody rb;

    // check if it's a movable target
    [SerializeField] private bool isMovableTarget;

    [SerializeField] private Vector3[] wayPoints;
    int currentWaypoint = 0;
    [SerializeField] private float moveSpeed;


    protected override void Start()
    {
        base.Start();
        colliderComponent = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void Update()
    {
        if (isMovableTarget)
        {
            Movement();
        }
    }

    public void Movement()
    {
        if (Vector3.Distance(transform.position, wayPoints[currentWaypoint]) < 0.25f)
        {
            currentWaypoint += 1;
            currentWaypoint = currentWaypoint % wayPoints.Length;
        }

        Vector3 directionOfMovement = (wayPoints[currentWaypoint] - transform.position).normalized;
        rb.MovePosition(transform.position + directionOfMovement * moveSpeed * Time.deltaTime);
        
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
