using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] SpriteList characterSprite;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] WayPoint startPoint;
    NavMeshAgent agent;
    Animator animator;
    [ReadOnly, SerializeField] WayPoint _targetWayPoint;

    bool canShoot = true;
    public bool CanShoot => canShoot;
    private void Awake()
    {
        if (startPoint != null)
            transform.position = startPoint.transform.position;
        _targetWayPoint = startPoint;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WayPoint"))
        {
            WayPoint wayPoint = other.GetComponent<WayPoint>();
            if (wayPoint != null && wayPoint == _targetWayPoint)
            {
                _targetWayPoint = null;
                wayPoint.TriggerEntering(this);
            }
        }
    }

    public void GoToWayPoint(WayPoint wayPoint)
    {
        if (wayPoint == null) return;
        _targetWayPoint = wayPoint;
        agent.isStopped = false;
        agent.SetDestination(wayPoint.transform.position);
        canShoot = false;
        characterSprite.SetSprite(0);
    }
    public void StopWalking()
    {
        agent.isStopped = true;
        canShoot = true;
        characterSprite.SetSprite(1);
    }
    
    public Transform GetBulletSpawnPoint()
    {
        return bulletSpawnPoint;
    }
}
