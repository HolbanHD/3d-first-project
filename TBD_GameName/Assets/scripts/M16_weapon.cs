using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a child of Weapon.
/// script specific to this weapon that sits in a prefab and holds its data in the inspector.
/// </summary>

public class M16_weapon : Weapon
{
    //__________________________________________________________________________ Variables

    [SerializeField] private float caseSpread;
    [SerializeField] private float caseVelocity;

    [SerializeField] Transform magPosition;

    [SerializeField] private GameObject bulletCaseType;
    [SerializeField] private GameObject magToDrop;

    //__________________________________________________________________________ Run


    //__________________________________________________________________________ Methods

    protected override void Shoot()
    {
        base.Shoot();
        Vector3 caseDirNoSpread = transform.right;

        float x = Random.Range(-caseSpread, caseSpread);
        float y = Random.Range(-caseSpread, caseSpread);
        
        Vector3 caseDirWithSpread = caseDirNoSpread + new Vector3 (-x,y,0);

        GameObject bullet = Instantiate(projectileType, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(caseDirWithSpread.normalized * caseVelocity, ForceMode.Impulse);
    }

    protected override void Reload()
    {
        base.Reload();
        magToDrop = Instantiate(magToDrop, magPosition.position,magPosition.transform.rotation);
    }

}

