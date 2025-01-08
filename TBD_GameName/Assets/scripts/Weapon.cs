using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour , IShootable
{
    //__________________________________________________________________________ Variables

    [SerializeField] protected string gunType;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int projectileDamage;
    [SerializeField] protected float projectileVelocity;
    [SerializeField] protected float rateOfFire;
    [SerializeField] protected float spread;
    [SerializeField] protected int magSize;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected bool shooting, readyToShoot, reloading , fireModeAuto;
    //[SerializeField] protected string fireMode;

    [SerializeField] protected int bulletsShot;

    [SerializeField] protected Camera aimCamera;
    [SerializeField] protected Transform shootingPoint;
    [SerializeField] protected GameObject projectileType;

    private bool allowInvoke = true;

    //__________________________________________________________________________ Run

    private void Awake()
    {
        currentAmmo = magSize;
        readyToShoot = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //__________________________________________________________________________ Methods

    private void GunInput()
    {
        if (fireModeAuto) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting && !reloading && currentAmmo > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    protected void SetGunType( string gunType){this.gunType = gunType;}

    public void Reload()
    {
        //reloading
    }

    public void Shoot()
    {
        readyToShoot = false;

        Ray ray = aimCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit)) targetPoint = hit.point;
        else targetPoint = ray.GetPoint(75);

        Vector3 directionNoSpread = targetPoint - shootingPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionNoSpread + new Vector3(-x, y, 0);

        GameObject bullet = Instantiate(projectileType, shootingPoint.position, Quaternion.identity);
        bullet.transform.forward = directionWithSpread .normalized.normalized;

        bullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * projectileVelocity,ForceMode.Impulse);

        currentAmmo--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke(nameof(ResetShoot), rateOfFire);
            allowInvoke = false;
        }
    }

    private void ResetShoot()
    {
        
    }
}
