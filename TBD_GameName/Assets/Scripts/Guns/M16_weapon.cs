using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a child of Weapon.
/// script specific to this weapon that sits in a prefab and holds its data in the inspector.
/// </summary>

namespace Guns
{

    public class M16_weapon : Weapon
    {
        //__________________________________________________________________________ Variables

        [SerializeField] private float caseSpread;
        [SerializeField] private float caseVelocity;

        [SerializeField] Transform magPosition;

        [SerializeField] private GameObject bulletCaseType;
        [SerializeField] private GameObject magToDrop;

        Rigidbody caseRB;

        //__________________________________________________________________________ Run


        //__________________________________________________________________________ Methods

        protected override void Shoot()
        {
            base.Shoot();
            Vector3 caseDirNoSpread = transform.right * -1;

            float x = Random.Range(-caseSpread, caseSpread);
            float y = Random.Range(-caseSpread, caseSpread);

            Vector3 caseDirWithSpread = caseDirNoSpread + new Vector3(-x, y, 0);

            GameObject bulletCaseSpawnd = Instantiate(bulletCaseType, transform.position, magPosition.transform.localRotation);
            caseRB = bulletCaseSpawnd.GetComponent<Rigidbody>();
            caseRB.AddForce(caseDirWithSpread.normalized * caseVelocity, ForceMode.Impulse);
            caseRB.AddTorque(new Vector3(0, 0, 1), ForceMode.Impulse);
        }

        protected override void Reload()
        {
            base.Reload();
            GameObject magToDropKKK = Instantiate(magToDrop, magPosition.position, magPosition.transform.rotation);
            Destroy(magToDropKKK, 10);
        }

    }

}

