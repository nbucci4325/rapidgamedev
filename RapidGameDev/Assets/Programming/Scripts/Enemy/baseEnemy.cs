using UnityEngine;

public abstract class baseEnemy : MonoBehaviour
{
    private float health = 100.0f;
    private string colourName;

    #region Setters
    /// <summary>
    /// Subtracts incoming attack damage from current health
    /// </summary>
    /// <param name="amount">The amount to be subtracted</param>
    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0.0f)
        {
            Debug.Log("Enemy Destroyed");
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Sets the colour of the enemy as a string
    /// </summary>
    /// <param name="colour">The colour, as a string, to be set</param>
    public void SetColour(string colour)
    {
        colourName = colour;
    }
    #endregion

    #region Collision Logic
    /// <summary>
    /// Describes how to be behave based on incoming attack, taking into account colour
    /// </summary>
    /// <param name="other">The attacking collider</param>
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger fired! Collided with: " + other.name);

        Transform weaponsParent = other.transform.root.Find("WeaponHolder");
        if (weaponsParent == null)
        {
            Debug.Log("WeaponsHolder not found!");
            return;
        }

        swordController sword = weaponsParent.GetComponentInChildren<swordController>(true);
        hammerController hammer = weaponsParent.GetComponentInChildren<hammerController>(true);
        wbxBowController bow = weaponsParent.GetComponentInChildren<wbxBowController>(true);

        MonoBehaviour activeWeapon = null;

        if (sword != null && sword.gameObject.activeInHierarchy)
            activeWeapon = sword;
        else if (hammer != null && hammer.gameObject.activeInHierarchy)
            activeWeapon = hammer;
        else if (bow != null && bow.gameObject.activeInHierarchy)
            activeWeapon = bow;

        if (activeWeapon == null)
        {
            Debug.Log("No active weapon detected!");
            return;
        }

        Debug.Log("Active weapon detected: " + activeWeapon.name);

        string weaponTag = activeWeapon.gameObject.tag;
        Transform tagChild = transform.GetChild(0);
        string enemyTag = tagChild.tag;

        Debug.Log("Enemy tag: " + enemyTag);
        Debug.Log("Weapon tag: " + weaponTag);

        if (weaponTag == enemyTag)
        {
            Debug.Log("HIT!");

            float weaponDamage = 0f;
            if (activeWeapon is swordController s) weaponDamage = s.damage;
            else if (activeWeapon is hammerController h) weaponDamage = h.damage;
            else if (activeWeapon is wbxBowController b) weaponDamage = b.damage;

            takeDamage(weaponDamage);
        }
    }
    #endregion
}

//public abstract void attack();
//public abstract void getCurrentColour();

