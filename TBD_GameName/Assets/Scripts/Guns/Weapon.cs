using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

/// <summary>
/// an abstract class holding the basic every weapon data and functions
/// </summary>

public abstract class Weapon : MonoBehaviour//, //IShootable
{
    //__________________________________________________________________________ Variables

    [SerializeField] protected string gunType;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int projectileDamage;
    [SerializeField] protected float projectileVelocity;
    [SerializeField] protected float rateOfFire;
    [SerializeField] protected float spread;
    [SerializeField] protected int magSize;
    //[SerializeField] protected float bulletsInShotgunShell;
    [SerializeField] protected bool triggerPulled;
    [SerializeField] protected bool fireModeAuto;
    [SerializeField] protected bool gunCycled;
    [SerializeField] protected bool reloading;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected int bulletsShot;

    [SerializeField] protected Camera mainCamera;

    [SerializeField] protected Transform gunTransform;
    [SerializeField] protected Transform shootingPoint;
    [SerializeField] protected GameObject projectileType;

    //[SerializeField] protected GameObject muzzleFlash;
    //[SerializeField] protected TextMeshProUGUI ammoDisplay;

    private bool allowInvoke = true;
    Vector3 targetPoint;

    //__________________________________________________________________________ Run

    private void Awake()
    {
        currentAmmo = magSize;
        gunCycled = true;
    }

    void Update()
    {
        GunInput();
    }

    //__________________________________________________________________________ Methods

    //inputs for shooting and reloading.
    protected void GunInput()
    {
        //input by gun shooting mode.
        if (fireModeAuto) triggerPulled = Input.GetKey(KeyCode.Mouse0);
        else triggerPulled = Input.GetKeyDown(KeyCode.Mouse0);

        //checking in gun done cycling between shots, there is input, not mid reloading and bullets in current mag is not 0.
        if (gunCycled && triggerPulled && !reloading && currentAmmo > 0)
        {
            bulletsShot = 0;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magSize && !reloading)
        {
            Reload();
        }
    }

    //spawn a bullet at shootingPint transform to direction of middle of screen, adding force from this script. (not form bullet at "start")
    protected virtual void Shoot()
    {
        gunCycled = false;

        //rey to middle of screen
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //if hit object else: distance of 75
        if (Physics.Raycast(ray, out hit)) targetPoint = hit.point;
        else targetPoint = ray.GetPoint(75);

        Vector3 directionNoSpread = targetPoint - shootingPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionNoSpread + new Vector3(-x, y, 0);

        // spawn, direction , getting bullet rigidbody and adding impulse force.
        GameObject bullet = Instantiate(projectileType, shootingPoint.position, Quaternion.identity);
        bullet.transform.forward = directionWithSpread.normalized;
        bullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * projectileVelocity, ForceMode.Impulse);

        currentAmmo--;
        bulletsShot++;

        //timing between shots.
        if (allowInvoke)
        {
            Invoke(nameof(ResetShoot), rateOfFire);
            allowInvoke = false;
        }

    }

    protected void ResetShoot()
    {
        gunCycled = true;
        allowInvoke = true;
    }

    protected virtual void Reload()
    {
        reloading = true;
        Invoke(nameof(ReloadingDone), reloadTime);
    }

    protected void ReloadingDone()
    {
        currentAmmo = magSize;
        reloading = false;
    }

    protected void RotateGunOnUp()
    {

    }
}


    /*private void MuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash,shootingPoint.position, Quaternion.identity);
        }
    }*/

        //shottgun
        /*        if (bulletsShot < bulletsInShootgunShell && currentAmmo > 0)
                {
                    Invoke(nameof(Shoot), rateOfFire);
                }*/