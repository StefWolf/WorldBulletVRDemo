using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionArea : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> areas;

    public void TransitionToTowerA(GameObject origin)
    {
        origin.transform.position = areas[0].transform.position;
    }

    public void TransitionToTowerB(GameObject origin)
    {
        origin.transform.position = areas[1].transform.position;
    }

    public void TransitionToTower(GameObject origin)
    {
        origin.transform.position = areas[2].transform.position;
    }

    public void TransitionToGate(GameObject origin)
    {
        origin.transform.position = areas[3].transform.position;
    }

    public void TransitionToStorage(GameObject origin)
    {
        origin.transform.position = areas[4].transform.position;
    }

}
