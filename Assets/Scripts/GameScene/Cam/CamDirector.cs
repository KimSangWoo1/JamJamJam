using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDirector : MonoBehaviour
{
    [SerializeField]
    List<Camera> camList;

    void Start()
    {
        for(int i=0; i< camList.Count; ++i)
        {
            Debug.Log(i);    
        }

        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        Commnad();
    }

    private void Initialization()
    {
        ActiveCam(0);
    }

    void ActiveCam(int num)
    {
        if(num >= camList.Count)
        {
            return;
        }

        for (int i = 0; i < camList.Count; i++)
        {
            if (i == num)
            {
                camList[i].gameObject.SetActive(true);
            }
            else
            {
                camList[i].gameObject.SetActive(false);
            }

        }
    }

    private void Commnad()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiveCam(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiveCam(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActiveCam(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActiveCam(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ActiveCam(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ActiveCam(5);
        }
    }
}
