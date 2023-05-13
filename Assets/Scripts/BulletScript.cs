using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    [SerializeField] int _damage = 1;

    public void Init(Vector3 target)
    {
        GetComponent<Rigidbody>().velocity = (target - transform.position).normalized * _speed;
        transform.LookAt(target);
        Invoke("Deactivate", 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ENEMY"))
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            
            if (enemy != null)
                enemy.GetDamage(_damage);
            
            Deactivate();
        }
    }
    
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
