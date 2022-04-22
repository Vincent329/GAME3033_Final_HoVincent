using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    void Start()
    {
        rb.velocity = transform.forward * bulletSpeed;
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }

    private void OnTriggerEnter(Collider other)
    {
        // pass reference of the player to the target
        if (other.GetComponent<TargetComponent>() != null)
        {
            other.GetComponent<TargetComponent>().TargetTakeDamage(15.0f, player.transform);
        }
        Destroy(gameObject);
    }
}
