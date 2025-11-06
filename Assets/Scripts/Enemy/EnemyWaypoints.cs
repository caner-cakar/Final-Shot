using UnityEngine;
using System.Collections.Generic;

public class EnemyWaypoints : MonoBehaviour
{
    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();
    public List<GameObject> Waypoints => waypoints;

    void Start()
    {
        foreach (GameObject wp in waypoints)
        {
            if (wp != null)
            {
                wp.transform.parent = null;
            }
        }
    }
}
