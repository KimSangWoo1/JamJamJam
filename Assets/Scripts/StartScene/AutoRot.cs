using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRot : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed;

    void LateUpdate()
    {
        transform.Rotate(Vector3.up *Time.deltaTime *rotSpeed , Space.Self);
    }
}
