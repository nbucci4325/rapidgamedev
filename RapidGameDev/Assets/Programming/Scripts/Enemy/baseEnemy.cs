using System;
using UnityEngine;

public abstract class baseEnemy : MonoBehaviour
{
    private float health = 100.0f;
    [HideInInspector]
    private bool isAlive = true;

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0.0f)
        {
            Debug.Log("Enemy Destroyed");
            isAlive = false;
        }
    }

    public abstract void attack();
}
