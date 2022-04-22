using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleMagnet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        HealthPickup healthDetect = other.GetComponent<HealthPickup>();
        if (healthDetect != null)
        {
            healthDetect.FindTarget(this.transform);
        }
    }
}
