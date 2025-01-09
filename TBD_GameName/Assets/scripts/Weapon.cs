using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;

//public abstract class Weapon : MonoBehaviour, IShootable
public class Weapon : MonoBehaviour, IShootable
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
    //[SerializeField] protected float bulletsInShootgunShell;
    [SerializeField] protected bool shooting, readyToShoot, reloading, fireModeAuto;
    //[SerializeField] protected string fireMode;

    [SerializeField] protected int bulletsShot;

    [SerializeField] protected Camera aimCamera;
    [SerializeField] protected Transform shootingPoint;
    [SerializeField] protected GameObject projectileType;

    //[SerializeField] protected GameObject muzzleFlash;
    //[SerializeField] protected TextMeshProUGUI ammoDisplay;

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
        GunInput();
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

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magSize && !reloading)
        {
            Reload();
        }
    }

    //protected void SetGunType(string gunType) { this.gunType = gunType; }

    public void Reload()
    {
        //reloading
        reloading = true;
        Invoke(nameof(ReloadingDone), reloadTime);
    }

    private void ReloadingDone()
    {
        currentAmmo = magSize;
        reloading = false;
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
        bullet.transform.forward = directionWithSpread.normalized.normalized;

        bullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * projectileVelocity, ForceMode.Impulse);

        currentAmmo--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke(nameof(ResetShoot), rateOfFire);
            allowInvoke = false;
        }

        //shottgun
/*        if (bulletsShot < bulletsInShootgunShell && currentAmmo > 0)
        {
            Invoke(nameof(Shoot), rateOfFire);
        }*/
    }

    private void ResetShoot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    /*private void UI_ShowAmmo()
    {
        if (ammoDisplay != null)
        {
            ammoDisplay.SetText(currentAmmo / bulletsInShootgunShell + "/" + magSize / bulletsInShootgunShell);
        }
    }*/

    /*private void MuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash,shootingPoint.position, Quaternion.identity);
        }
    }*/
}
