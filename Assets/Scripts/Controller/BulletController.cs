using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BulletController : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform CanoArma;
    public TextMeshProUGUI textAmount;
    public GameObject Bullet;
    public ParticleSystem particles;
    private bool isTriggerAlreadyClicked = false;
    [SerializeField]
    private TouchController touchController;

    private int amount = 0;

    private Cartucho cartucho;       
    void Start()
    {
        particles.Stop();
    }
    
    public void GrabSelect()
    {
        if (touchController.IsContinuousClickedWithRightHand())
        {
            Debug.Log("Clicar");
            HandleTriggerClick();
        }
    }

    private void HandleTriggerClick()
    {
        if(amount > 0)
        {
            audioSource.Play();
            amount = amount - 1;
            textAmount.text = amount.ToString();
            GameObject newBullet = Instantiate(Bullet, CanoArma.position, CanoArma.rotation);
            particles.Play();
            Destroy(newBullet, 2f);
            Invoke("DisableParticle", 0.2f);
            Invoke("AtivarTiro", 0.5f);
        }
    }

    private void AtivarTiro()
    {
        isTriggerAlreadyClicked = false;
    }

    private void DisableParticle()
    {
        particles.Stop();
    }

    public void PutBulletInWeapon(SelectEnterEventArgs cart)
    {
        Cartucho _c = cart.interactableObject.transform.GetComponent<Cartucho>();
        cartucho = _c;
        amount = _c.amount;
        textAmount.text = "50";
    }
}
