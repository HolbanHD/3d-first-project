using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Guns
{

    public class BulletCaseScript : MonoBehaviour
    {

        [SerializeField] int destroyTime = 10;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Destroy(gameObject, destroyTime);
        }
    }

}

