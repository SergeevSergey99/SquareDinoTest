using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    ObjectPool _pool = null;

    public void Init(ObjectPool pool)
    {
        _pool = pool;
    }

    private void OnDisable()
    {
        if (_pool != null)
        {
            _pool.ReturnObject(gameObject);
        }
    }
}
