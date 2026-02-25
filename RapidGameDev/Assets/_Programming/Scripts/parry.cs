using System.Collections;
using UnityEngine;

public class parry : MonoBehaviour
{
    [SerializeField]
    public GameObject parryCollider;

    [SerializeField]
    private playerHealth health;
    public float cooldown = 5.0f;
    public bool parryOn = false;
    public bool canParry = true;

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canParry)
        {
            doAParry();
        }
    }

    public void doAParry()
    {
        parryCollider.SetActive(true);
        parryOn = true;
        health.parryActive = true;
        canParry = false;
        Invoke("NoParry", 1f);
        StartCoroutine(ParryCooldown());
    }

    public void NoParry()
    {
        parryCollider.SetActive(false);
        parryOn = false;
        health.parryActive = false;
    }

    IEnumerator ParryCooldown()
    {
        yield return new WaitForSeconds(cooldown + 1f);
        canParry = true;
    }
}
