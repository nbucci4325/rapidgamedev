using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPool : MonoBehaviour
{
    public GameObject prefab;
    private Queue<GameObject> pool = new Queue<GameObject> ();

    public GameObject getObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue ();
            obj.SetActive (true);
            return obj;
        }
        return Instantiate(prefab);
    }

    public void returnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue (obj);
    }
}
