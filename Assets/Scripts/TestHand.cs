using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class TestHand : MonoBehaviour
{   
    private bool isTriggerAlreadyClicked = false;
    public bool isLeftHand = false;
    public float clickThreshold = 0.5f; 
    public AudioSource audioSource;
    public Transform CanoArma;
    public GameObject Bullet;
    

    public ParticleSystem particles;

     void Start()
    {   

        particles.Stop();
    }

    private void Update()
    {
        InputDevice targetDevice;
        var devices = new List<InputDevice>();

        InputDevices.GetDevicesAtXRNode(isLeftHand ? XRNode.LeftHand : XRNode.RightHand, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];

            if (targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isTriggerClicked) && isTriggerClicked)
            {
 
                if (!isTriggerAlreadyClicked)
                {
                    isTriggerAlreadyClicked = true;
                    HandleTriggerClick();
                }
            }
        }
    }

    private void HandleTriggerClick()
    {
        audioSource.Play();
        
        GameObject newBullet = Instantiate(Bullet, CanoArma.position, CanoArma.rotation);
        particles.Play();
        Destroy(newBullet, 2f);
        Invoke("DisableParticle", 0.2f);
        Invoke("AtivarTiro", 0.5f);
    }

    private void AtivarTiro() {
        isTriggerAlreadyClicked = false;
    }

    private void DisableParticle() {
        particles.Stop();
    }
    
}
