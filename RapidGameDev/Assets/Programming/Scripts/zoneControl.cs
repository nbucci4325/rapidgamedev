using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class zoneControl : MonoBehaviour
{
    [SerializeField] private int zoneQuota = 0;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private bool firstZoneInLevel = false;

    private void Start()
    {
        if (!firstZoneInLevel) foreach (GameObject spawner in spawners) spawner.SetActive(false);
    }

    private void Update()
    {
        //if enemy dies, call decrementZoneQuota() and check if zoneQuota is 0 or less, if so call zoneComplete()
        if (zoneQuota <= 0) zoneComplete();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            //play animation of door opening here
            foreach (GameObject spawner in spawners)
            {
                spawner.SetActive(true);
            }
        }
    }

    private void zoneComplete()
    {
        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(false);
        }
    }

    private void decrementZoneQuota()
    {
        zoneQuota--;
    }
}
