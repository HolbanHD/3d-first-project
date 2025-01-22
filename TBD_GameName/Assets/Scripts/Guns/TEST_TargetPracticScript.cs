using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_TargetPracticScript : MonoBehaviour,IDamagable
{

    [SerializeField] private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }
}
