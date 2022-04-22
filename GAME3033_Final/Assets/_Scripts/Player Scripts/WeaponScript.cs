using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WeaponScript : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField] private GameObject bulletToSpawn;
    [SerializeField] private Transform spawnLocation;

    // muzzle Flash
    [SerializeField] private ParticleSystem particleVFX;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FireWeapon(Transform locationToAim)
    {
        if (particleVFX != null)
        {
            particleVFX.Play();
        }
        if (audioSource != null)
        {
            audioSource.Play();
        }
        Vector3 aimVector = locationToAim.position - spawnLocation.position;
        Vector3 normalizedAimVector = aimVector.normalized;
        GameObject bullet = Instantiate(bulletToSpawn, spawnLocation.position, Quaternion.LookRotation(normalizedAimVector, Vector3.up));
        bullet.GetComponent<BulletComponent>().SetPlayer(this.gameObject);
    }
}
