using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField] private GameObject bulletToSpawn;
    [SerializeField] private Transform spawnLocation;

    // muzzle Flash
    [SerializeField] private ParticleSystem particleVFX;

    public void FireWeapon(Transform locationToAim)
    {
        if (particleVFX != null)
        {
            particleVFX.Play();
        }
        Vector3 aimVector = locationToAim.position - spawnLocation.position;
        Vector3 normalizedAimVector = aimVector.normalized;
        GameObject bullet = Instantiate(bulletToSpawn, spawnLocation.position, Quaternion.LookRotation(normalizedAimVector, Vector3.up));
        bullet.GetComponent<BulletComponent>().SetPlayer(this.gameObject);
    }
}
