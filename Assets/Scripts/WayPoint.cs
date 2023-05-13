using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public float size = 1;
    public List<WayPoint> neighbors = new List<WayPoint>();
    public List<EnemyScript> enemiesOnPoint;
    CharacterScript _player = null;

    private void Awake()
    {
        foreach (var enemy in enemiesOnPoint)
        {
            if (enemy != null)
                enemy.Init(this);
        }
    }

    public void TriggerEntering(CharacterScript player)
    {
        _player = player;
        if (_player != null)
        {
            _player.StopWalking();

            foreach (var enemy in enemiesOnPoint)
            {
                if (enemy != null)
                    enemy.ShowCanvas();
            }

            CheckEnemies();
        }
    }

    public void EnemyDied(EnemyScript enemy)
    {
        enemiesOnPoint.Remove(enemy);
        CheckEnemies();
    }

    public void CheckEnemies()
    {
        if (enemiesOnPoint.Count == 0)
        {
            if (_player != null)
                _player.GoToWayPoint(GetRandomNeighbor());
        }
    }

    WayPoint GetRandomNeighbor()
    {
        if (neighbors.Count == 0) return null;
        return neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
    }

    WayPoint GetNeighbor(int index)
    {
        if (index < 0 || index >= neighbors.Count) return null;
        return neighbors[index];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, Vector3.one * size);

        Gizmos.color = Color.green;
        foreach (var neighbor in neighbors)
        {
            if (neighbor != null)
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }

        Gizmos.color = Color.red;
        foreach (var enemy in enemiesOnPoint)
        {
            if (enemy != null)
                Gizmos.DrawLine(transform.position, enemy.transform.position);
        }
    }
}