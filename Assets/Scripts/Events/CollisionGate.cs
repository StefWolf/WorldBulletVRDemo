using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGate : MonoBehaviour
{
    [SerializeField]
    private Gate gate;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie")) {
            if(gate.GetHp() > 0) {
                gate.LosingLife();
            } else
            {
                Destroy(gameObject);
            }
        }
    }

}
