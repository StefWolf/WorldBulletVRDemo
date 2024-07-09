using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BodyPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Player levando dano: ");
            player.RemoveHP(10);
        }
    }
}
