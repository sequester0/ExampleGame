using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cleaner"))
        {
            Destroy(this.gameObject);
        }
    }
}
