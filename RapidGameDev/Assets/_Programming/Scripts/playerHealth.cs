using System.Collections;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int health = 100;
    public bool parryActive = false;
    private bool damageTaken = false;
    private int damageAmount;
    private bool canHeal = false;
    private bool isHealing = false;
    public float healDelay = 1f;

    [SerializeField]
    public Transform hurtbox;
    
    // Update is called once per frame
    void Update()
    {
        if(health < 100)
        {
            canHeal = true;
            if (!isHealing)
                isHealing = true;
                Invoke("InitHeal", 5f);
        }
        
        if (damageTaken)
        {
            if (parryActive)
                damageAmount = 0;

            health -= damageAmount;
            Debug.Log("Health: " + health);
            damageTaken = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collider Activated");
            damageTaken = true;
            damageAmount = 5;
        }
    }

    public void InitHeal()
    {
        while (canHeal)
        {
            AutoHeal();
            if (health == 100)
            {
              canHeal = false;
              isHealing = false;
            }
        }

        healDelay = 1f;
    }

    public void AutoHeal()
    {
        health++;
        Debug.Log("Current health after heal: " + health);
        HealTickCooldown();
    }

    IEnumerator HealTickCooldown()
    {
        yield return new WaitForSeconds(healDelay);
    }
}
