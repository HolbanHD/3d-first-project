using interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Guns
{

    public class BulletScript : MonoBehaviour
    {

        [SerializeField] int destroyTime = 3;
        void Start()
        {

        }

        void Update()
        {
            Destroy(gameObject, destroyTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable iDamageable))
            {
                iDamageable.TakeDamage(10);
            }
            Destroy(gameObject);
        }

    }

}
