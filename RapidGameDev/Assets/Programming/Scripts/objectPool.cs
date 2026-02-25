using System.Collections.Generic;
using UnityEngine;

public class objectPool : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] List<GameObject> pool = new List<GameObject>();

    public GameObject getObject()
    {
        foreach (GameObject g in pool)
        {
            if (!g.activeInHierarchy)
            {
                g.SetActive(true);
                return g;
            }
        }
        GameObject ins = Instantiate(prefab, this.transform);
        pool.Add(ins);
        return ins;
    }

    public void returnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
