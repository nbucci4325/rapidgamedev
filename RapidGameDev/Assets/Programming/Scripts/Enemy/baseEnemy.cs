using System;
using UnityEditor.UI;
using UnityEngine;

public abstract class baseEnemy : MonoBehaviour
{
    private float health = 100.0f;
    private string colourName;

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0.0f)
        {
            Debug.Log("Enemy Destroyed");
            gameObject.SetActive(false);
        }
    }
    public void SetColour(string colour)
    {
        colourName = colour;
    }


    public void OnTriggerEnter(Collider other)
    {
        swordController sword = other.GetComponentInParent<swordController>();
        if (sword == null)
            return;

        string swordTag = sword.gameObject.tag;
        Transform tagChild = transform.GetChild(0);
        string enemyTag = tagChild.tag;

        Debug.Log("Enemy tag: " + enemyTag);
        Debug.Log("Sword tag: " + swordTag);

        if (swordTag == enemyTag)
        {
            Debug.Log("HIT!");
            takeDamage(sword.damage);
        }
    }
}

//public abstract void attack();
//public abstract void getCurrentColour();

