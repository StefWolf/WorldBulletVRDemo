using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionHead : MonoBehaviour
{
    [SerializeField]
    private ZombieNPC zombie;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Acertou na cabeça");
        zombie.SetLife(zombie.GetLife() - 100);
    }
}
