using UnityEngine;

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
        if (zoneQuota <= 0)
        {
            Debug.Log("ZONE COMPLETE");
            zoneComplete();
        }
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

    public void decrementZoneQuota()
    {
        zoneQuota--;
        Debug.Log("QUOTA DECREMENTED");
    }
}
