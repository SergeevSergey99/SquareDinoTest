using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvents : MonoBehaviour
{
    public string Tag = "Player";
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onCollisionEnter;
    public UnityEvent onCollisionExit;
    
    private void OnTriggerEnter(Collider other)
    {
        if (Tag == "" || other.CompareTag(Tag))
            onTriggerEnter.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if (Tag == "" || other.CompareTag(Tag))
            onTriggerExit.Invoke();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (Tag == "" || other.gameObject.CompareTag(Tag))
            onCollisionEnter.Invoke();
    }
    private void OnCollisionExit(Collision other)
    {
        if (Tag == "" || other.gameObject.CompareTag(Tag))
            onCollisionExit.Invoke();
    }
}
