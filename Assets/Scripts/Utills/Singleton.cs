using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
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

    public virtual void Awake()
    {
        if (_instance != null)
        {
            if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        _instance = GetComponent<T>();

        DontDestroyOnLoad(gameObject);

        if (_instance == null)
        {
            return;
        }
    }
}
