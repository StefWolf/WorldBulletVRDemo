using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    [SerializeField]
    private GameObject item;

    [SerializeField]
    private int price;

    [SerializeField]
    private PlayerController playerController;

    public void BuyItem()
    {
        if(playerController.GetMoneys() >= price)
        {
            playerController.RemoveMoney(price);

            Transform transform = GameObject.Find("spawnItemsDrop").transform;

            Instantiate(item, transform.position, transform.rotation);

        } else
        {
            Debug.LogError("Não tem moneys suficiente");
        }
    }

    public void BuyRepairTheWall(Gate gate)
    {
        if (playerController.GetMoneys() >= price)
        {
            playerController.RemoveMoney(price);
            gate.RepairTheWall(10);

        }
        else
        {
            Debug.LogError("Não tem moneys suficiente");
        }
    }
}
