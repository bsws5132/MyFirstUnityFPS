using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtHealthManager : MonoBehaviour
{
    public float hitpoint = 100f;
    public float respawnHitpoint = 100f;

    public void ApplyDamage(float damage)
    {
        hitpoint -= damage;

        if (hitpoint <= 0)
        {
            transform.position = TargetBounds.Instance.GetRandomPosition();
            hitpoint = respawnHitpoint;

        }
    }
}
