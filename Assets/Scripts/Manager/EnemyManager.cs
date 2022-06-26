using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> redList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> blueList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> greenList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> yellowList = new List<GameObject>();

    public List<GameObject> RedList => redList;
    public List<GameObject> BlueList => blueList;
    public List<GameObject> GreenList => greenList;
    public List<GameObject> YellowList => yellowList;
}
