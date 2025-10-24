using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPos + GetRandomDir() * Random.Range(10f, 70f);
    }
}
