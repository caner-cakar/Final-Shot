using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    Rigidbody[] rbs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs) rb.isKinematic = true;
    }

    public void TriggerRagdoll()
    {
        foreach (Rigidbody rb in rbs) rb.isKinematic = false;
    }
}
