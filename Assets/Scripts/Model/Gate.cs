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

    private void Start()
    {
        hp = 1000;
        amountText.text = hp.ToString();
    }

    public void RepairTheWall(int amount)
    {
        this.hp += amount;
        amountText.text = amount.ToString();
    }

    public int GetHp() {
        return hp;
    }

    public void LosingLife() {

        if(hp > 0) {
            hp -= 50;
            amountText.text = hp.ToString();
        }
       
    }
}
