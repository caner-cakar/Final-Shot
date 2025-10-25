using UnityEngine;
using System.Collections.Generic;

public class EnemyWaypoints : MonoBehaviour
{
    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();
    public List<GameObject> Waypoints => waypoints;
}
