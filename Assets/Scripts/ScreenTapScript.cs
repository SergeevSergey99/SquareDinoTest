using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTapScript : MonoBehaviour
{
    public ObjectPool bulletPool;
    public CharacterScript character;
    Camera _camera;
    float _maxRayDistance = 10f;
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && character.CanShoot)
            {

                var bull = bulletPool.GetObject();
                bull.transform.position = character.GetBulletSpawnPoint().position;
                    
                var ray = _camera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, _maxRayDistance))
                {
                    bull.GetComponent<BulletScript>().Init(hit.point);
                }
                else
                {
                    bull.GetComponent<BulletScript>().Init(ray.GetPoint(_maxRayDistance));
                }
                
            } 
        }
    }
}
