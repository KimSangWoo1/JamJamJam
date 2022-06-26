using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Assets/SO/Channel/VoidEventChannel", order = 1)]
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
