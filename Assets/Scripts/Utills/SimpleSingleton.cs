using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object _Lock = new object();
    private static T _instance;

    public static T Instance
    {
        get
        {
            lock (_Lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                        //DontDestroyOnLoad(_instance);
                    }
                }
            }
            return _instance;
        }
    }
}
