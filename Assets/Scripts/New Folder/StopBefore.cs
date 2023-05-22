using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBefore : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(FreezeCoroutine(4f));
    }

    IEnumerator FreezeCoroutine(float duration)
    {
        // Freeze the object
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
            rigidbody.isKinematic = true;

        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Resume the object's functionality
        if (rigidbody != null)
            rigidbody.isKinematic = false;

        if (collider != null)
            collider.enabled = true;
    }
}
