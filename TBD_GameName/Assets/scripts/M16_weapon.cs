using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M16_weapon : Weapon
{
    //__________________________________________________________________________ Variables

    

    //__________________________________________________________________________ Run

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //__________________________________________________________________________ Methods

    private void xxx()
    {
        if (Input.GetMouseButtonDown(0))
        {
            projectileVelocity = 20;
            Shoot();
        }
    }
}
