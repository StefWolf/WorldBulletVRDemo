using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private TextMeshProUGUI amountText;

    public void RepairTheWall(int amount)
    {
        this.hp += amount;
        amountText.text = amount.ToString();
    }
}
