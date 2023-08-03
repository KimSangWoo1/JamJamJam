using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlankScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("GameScene");
    }
}
