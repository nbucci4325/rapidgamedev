using System;
using UnityEditor.UI;
using UnityEngine;

public abstract class baseEnemy : MonoBehaviour
{
    private float health = 100.0f;

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0.0f)
        {
            Debug.Log("Enemy Destroyed");
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        swordController temp = other.GetComponent<swordController>();
        string playerTag;
        string currentTag = gameObject.tag;
        if (temp) //&& playerTag == currentTag)
        {

        }
        {
            takeDamage(temp.damage);
        }
    }

    //public abstract void attack();
    //public abstract void getCurrentColour();
}
