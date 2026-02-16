using System.Collections;
using UnityEngine;

public class parry : MonoBehaviour
{
    [SerializeField]
    public GameObject parryCollider;
    public float cooldown = 5.0f;
    public bool parryOn = false;
    public bool canParry = true;

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !parryOn)
        {
            doAParry();
        }
    }

    public void doAParry()
    {
        parryCollider.SetActive(true);
        parryOn = true;
        canParry = false;
        Invoke("NoParry", 1f);
        StartCoroutine(ParryCooldown());
    }

    public void NoParry()
    {
        parryCollider.SetActive(false);
        parryOn = false;
        canParry = true;
    }

    IEnumerator ParryCooldown()
    {
        yield return new WaitForSeconds(cooldown);
    }
}
