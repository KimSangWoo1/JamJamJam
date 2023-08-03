using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpControl : MonoBehaviour
{
    [SerializeField]
    private Image[] hearts;

    private int index;

    private void OnEnable()
    {
        index = 0;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }
    }

    public void SetHP(int amount)
    {
        for(int i=0; i< amount; i++)
        {
            hearts[i].gameObject.SetActive(true);
            index = i;
        }
    }

    public void ChangeHP()
    {
        if (index >= 0)
        {
            hearts[index].gameObject.SetActive(false);
            index--;
        }
    }
}
