using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<ICollectable>(out ICollectable icollectable))
        {
            icollectable.Collect();
            Destroy(collision.gameObject);
        }
    }
}
