using System;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
        }
    }
}