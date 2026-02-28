using UnityEngine;

#region Struct
[System.Serializable]
public struct zone
{
    [SerializeField] GameObject spawners;
    [SerializeField] GameObject gates;
    [SerializeField] GameObject navMesh;

    public void setActive(bool state)
    {
        if (spawners != null) spawners.SetActive(state);
        if (gates != null) gates.SetActive(state);
        if (navMesh != null) navMesh.SetActive(state);
    }
}
#endregion

public class _zoneManager : MonoBehaviour
{
    [Tooltip("Input the quotas per zone here, in ascending order.")]
    [SerializeField] int[] quotas;
    [Tooltip("Every index of this array represents one zone. In each zone, include the parent object of the respective indexes.")]
    [SerializeField] zone[] zoneData;
    private int currentZoneQuota;

    #region Instantiation and Signalling
    private void Start()
    {
        setActiveZone(0);
        currentZoneQuota = 0;
    }

    private void setActiveZone(int index)
    {
        if (index < 0 || index >= zoneData.Length) return;
        for (int i = 0; i < zoneData.Length; i++) zoneData[i].setActive(false);
        zoneData[index].setActive(true);
    }

    public void decrementQuota() 
    {
        quotas[currentZoneQuota]--;
        if (quotas[currentZoneQuota] <= 0) zoneComplete();
    }

    private void zoneComplete() 
    {
        currentZoneQuota++;
        if (currentZoneQuota >= zoneData.Length)
        {
            Debug.Log("All zones complete");
            return;
        }
        setActiveZone(currentZoneQuota);
    }
    #endregion
}