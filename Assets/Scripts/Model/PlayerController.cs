using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    private Player player;

    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerHp;
    public TextMeshProUGUI playerMoneys;
    public TextMeshProUGUI playerHunger;
    public AudioSource playerDamage;

    [SerializeField]
    private CanvasGroup damageView;

    [SerializeField]
    private ParticleSystem cureParticle;

    [SerializeField]
    private TouchController touchController;

    [SerializeField]
    private string name;

    /**
     * Verify if player has dead or not
     */
    public bool VerifyHP()
    {
        if (player.hp <= 0)
            return false;
        return true;
    }

    void Start()
    {
        cureParticle.Stop();
    }

    public void RemoveHP(int amount) 
    {
        ProcessDamageViewStart();
        player.hp  -= amount;
        playerHp.text = player.hp.ToString();
        Invoke("ProcessDamageViewFinish", 3f);
    }

    void ProcessDamageViewStart()
    {
        playerDamage.Play();
        damageView.alpha = 1;
    }

    void ProcessDamageViewFinish()
    {
        playerDamage.Stop();
        damageView.alpha = 0;
    }

    public void AddHP(int amount) 
    { 
        player.hp += amount;
        playerHp.text = player.hp.ToString();
    }
    public void RemoveMoney(float amount) 
    {
        player.moneys -=amount; 
        playerMoneys.text = player.moneys.ToString();
    }
    public void AddMoney(float amount) 
    {
        Debug.Log("Add "+ amount.ToString());
        player.moneys += amount;
        playerMoneys.text = player.moneys.ToString();
    }

    public float GetMoneys()
    {
        return player.moneys;
    }

    /**
     * Render infos of menu
     */
    public void StartRenderTexts()
    {
        player = new(name);
        Debug.Log("Inicia renderização do Player");
        Debug.Log(player.name);
        playerHp.text = player.hp.ToString();
        playerHunger.text = player.hunger.ToString();
        playerMoneys.text= player.moneys.ToString();
        playerName.text = player.name.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colidiu com algo");
        if(collision.transform.tag == "cure")
        {
            Debug.Log("Colidiu com seringa aaaa");
        }
    }

    public void CurePlayer(ActivateEventArgs grabber)
    {
        if (touchController.IsClickedWithLeftHand())
        {
            Destroy(grabber.interactableObject.transform.gameObject);
            cureParticle.Play();
            AddHP(30);
        }

        if(touchController.IsClickedWithRightHand())
        {
            Destroy(grabber.interactableObject.transform.gameObject);
            cureParticle.Play();
            AddHP(30);
        }
    }

}


