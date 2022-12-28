using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHealth : MonoBehaviour
{
    public float hitpoint = 100f;
   

    public void ApplyDamage(float damage)
    {
        hitpoint -= damage;

        if (hitpoint <= 0)
        {
            GetComponent<ExplosiveBarrelScript>().explode = true;
            
        }
    }
}
