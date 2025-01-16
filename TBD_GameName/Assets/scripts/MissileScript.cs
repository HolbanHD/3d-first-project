using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissileScript : MonoBehaviour
{

    [SerializeField] private List<Collider> enemyInRange = new List<Collider> { };
    //[SerializeField] private List<IDamageable> enemyInRange = new List<IDamageable> { };
    [SerializeField] private float explosionRadius = 5;
    //Rigidbody enemyRB;
    [SerializeField] private float explosionForce = 10;
    [SerializeField] int destroyTime = 6;

    void Start()
    {

    }

    private void FixedUpdate()
    {

    }

    void Update()
    {
        //setTargetsInList();
        Destroy(gameObject, destroyTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        enemyInRange = Physics.OverlapSphere(transform.position, explosionRadius).ToList();

        foreach (Collider enemy in enemyInRange)
        {
            if (enemy.gameObject.TryGetComponent<IDamageable>(out IDamageable iDamageable))
            {
                Rigidbody enemyRB = enemy.gameObject.GetComponent<Rigidbody>();
                iDamageable.TakeDamage(10);

                if (enemyRB != null)
                {
                    enemyRB.AddForce((enemy.transform.position - transform.position) + Vector3.up * explosionForce, ForceMode.Impulse);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }



    /*    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable iDamageable))
            {
                iDamageable.TakeDamage(10);
            }
            Destroy(gameObject);
        }*/


    /*    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable iDamageable))
            {
                iDamageable.TakeDamage(10);
            }
            Destroy(gameObject);
        }*/

}


