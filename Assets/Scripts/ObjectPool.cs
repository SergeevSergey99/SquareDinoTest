using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectPrefab;
    public List<GameObject> activeObjectsPool = new List<GameObject>();
    public List<GameObject> reservedObjectsPool = new List<GameObject>();
    
    public GameObject GetObject()
    {
        if (reservedObjectsPool.Count > 0)
        {
            GameObject obj = reservedObjectsPool[0];
            reservedObjectsPool.RemoveAt(0);
            activeObjectsPool.Add(obj);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.GetComponent<PooledObject>().Init(this);
            activeObjectsPool.Add(obj);
            obj.SetActive(true);
            return obj;
        }
    }
    
    public void ReturnObject(GameObject obj)
    {
        if (activeObjectsPool.Contains(obj))
        {
            activeObjectsPool.Remove(obj);
        } 
        
        reservedObjectsPool.Add(obj);
        obj.SetActive(false);
    }
    
}
