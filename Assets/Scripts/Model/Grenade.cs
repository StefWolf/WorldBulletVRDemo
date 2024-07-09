using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private int time;
    [SerializeField]
    private int distance;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private ParticleSystem particle;
}
