using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int health = 100;
    public bool parryActive = false;
    private bool damageTaken = false;
    private int damageAmount;

    [SerializeField]
    public Transform hurtbox;
    
    // Update is called once per frame
    void Update()
    {
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
}
