using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// a script siting on the missile prefab.
/// need to drag the layers manually in editor: - enemyLayer to enemyLayer - BlockExplosionLayer to everything except enemyLayer - .
/// 
/// on collision explode, Creates a physics sphere that adds colliders to list,
/// fires a ray that checks layers and if it hit an enemy, then it reaches the IDamageable and applies force to the object.
/// </summary>

public class MissileScript : MonoBehaviour
{

    [SerializeField] private List<Collider> enemyInRange = new List<Collider> { };
    [SerializeField] private float explosionRadius = 5;
    [SerializeField] private float explosionForce = 10;
    [SerializeField] int destroyTime = 6;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask BlockExplosionLayer;


    void Start()
    {

    }

    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
    private void OnCollisionEnter(Collision collision)
    {

        enemyInRange = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer).ToList();
        if (enemyInRange.Count > 0)
        {
            foreach (Collider enemy in enemyInRange)
            {
                Vector3 enemyDir = enemy.transform.position - transform.position;
                Physics.Raycast(transform.position, enemy.transform.position, out RaycastHit hit);
                Debug.DrawRay(transform.position, enemyDir, Color.green);
                float enemyDis = Vector3.Distance(transform.position, enemy.transform.position) * 100;

                if (!Physics.Raycast(transform.position, (enemy.transform.position - hit.point).normalized, enemyDis, BlockExplosionLayer))
                {
                    if (enemy.gameObject.TryGetComponent<IDamageable>(out IDamageable iDamageable))
                    {
                        Rigidbody enemyRB = enemy.gameObject.GetComponent<Rigidbody>();
                        iDamageable.TakeDamage(10);

                        //enemyRB.AddForce(enemyDir.normalized * (explosionForce - enemyDis)  * Time.deltaTime, ForceMode.Impulse);
                        enemyRB.AddExplosionForce(explosionForce - enemyDis, transform.position, explosionRadius);
                    }
                }
                Destroy(gameObject);
            }
        }
        else { Destroy(gameObject); }


        /*        enemyInRange = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer).ToList();

                if (enemyInRange.Count > 0)
                {
                    foreach (Collider enemy in enemyInRange)
                    {
                        //Vector3 enemyDir = enemy.transform.position - transform.position; // us for adding original force
                        float enemyDis = Vector3.Distance(transform.position, enemy.transform.position) * 100;

                        if (enemy.gameObject.TryGetComponent<IDamageable>(out IDamageable iDamageable))
                        {
                            Rigidbody enemyRB = enemy.gameObject.GetComponent<Rigidbody>();
                            iDamageable.TakeDamage(10);

                            //enemyRB.AddForce(enemyDir.normalized * (explosionForce - enemyDis)  * Time.deltaTime, ForceMode.Impulse);
                            enemyRB.AddExplosionForce(explosionForce-enemyDis,transform.position,explosionRadius) ;

                        }
                        Destroy(gameObject);
                    }
                }
                else { Destroy(gameObject); }*/
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);

    }

}


