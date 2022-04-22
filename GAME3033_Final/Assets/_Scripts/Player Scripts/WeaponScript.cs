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
    [SerializeField] private GameObject particleVFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireWeapon(Transform locationToAim)
    {

        Vector3 aimVector = locationToAim.position - spawnLocation.position;
        Vector3 normalizedAimVector = aimVector.normalized;
        Instantiate(bulletToSpawn, spawnLocation.position, Quaternion.LookRotation(normalizedAimVector, Vector3.up));

    }
}
