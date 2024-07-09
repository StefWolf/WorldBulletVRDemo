using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private int count = 0;

    [SerializeField]
    private TextMeshProUGUI m_TextMeshPro;

    private void OnTriggerEnter(Collider other)
    {
        count++;
        m_TextMeshPro.text = count.ToString();
    }
}
