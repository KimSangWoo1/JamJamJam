using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundClone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Ground.Instance.PushPool(gameObject);
    }
}
