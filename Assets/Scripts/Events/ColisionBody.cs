using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionBody : MonoBehaviour
{
    [SerializeField]
    private ZombieNPC zombie;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Acertou no corpo");
        zombie.SetLife(zombie.GetLife() - 10);
    }
}
