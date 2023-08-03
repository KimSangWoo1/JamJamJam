using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoidEventChannel : ScriptableObject
{
    public UnityAction OnRequested;

    public void Event()
    {
        if (OnRequested != null)
        {
            OnRequested.Invoke();
        }
        else
        {
            Debug.LogWarning("OnRequested Null");
        }
    }
}
