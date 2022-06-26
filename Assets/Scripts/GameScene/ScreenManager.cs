using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections.LowLevel.Unsafe;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private Button[] attackBtns;

    [SerializeField]
    private AttackChannel attackChannel;

    private void Start()
    {
        AttackType attackType;

        for(int i=0; i<attackBtns.Length; i++)
        {
            switch (i)
            {
                case 0:
                    attackBtns[i].onClick.AddListener(() => AttackClick(AttackType.RED));
                    break;
                case 1:
                    attackBtns[i].onClick.AddListener(() => AttackClick(AttackType.GREEN));
                    break;
                case 2:
                    attackBtns[i].onClick.AddListener(() => AttackClick(AttackType.BLUE));
                    break;
                case 3:
                    attackBtns[i].onClick.AddListener(() => AttackClick(AttackType.YELLOW));
                    break;
            }
        }
    }

    public void AttackClick(AttackType attack)
    {
        attackChannel.AttackEvent(attack);
    }
}
