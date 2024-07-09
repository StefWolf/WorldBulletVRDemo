using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;
    [SerializeField]
    private GameObject buttonShop;
    [SerializeField]
    private float minutesForFullRotation = 1.0f;

    [SerializeField]
    private List<GameObject> zombiesPrefabs;

    [SerializeField]
    private Canvas canvasGameOver;

    private float degreesPerSecond;

    private int day;

    private bool isDay = true;

    private bool isNight = true;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private List<GameObject> pointsOfSpawnZombie;

    public float spawnInterval = 2.0f;

    private float nextTime = 0.0f;

    [SerializeField]
    private AudioSource audioGlobal;

    void Start()
    {

        canvasGameOver.gameObject.SetActive(false);
        degreesPerSecond = 360.0f / (minutesForFullRotation * 60.0f);
        day = 0;
        playerController.StartRenderTexts();
        audioGlobal.Play();
    }

    void SpawnZombie()
    {
        System.Random random = new System.Random();
        int randomIndex = random.Next(0, pointsOfSpawnZombie.Count);
        int randomIndexZombies = random.Next(0, zombiesPrefabs.Count);
        Debug.Log("Spawnando zombie " + randomIndexZombies + " em " + pointsOfSpawnZombie[randomIndex].transform);

        Vector3 positionBornZombie = new Vector3(
            pointsOfSpawnZombie[randomIndex].transform.position.x, 
            pointsOfSpawnZombie[randomIndex].transform.position.y,
            pointsOfSpawnZombie[randomIndex].transform.position.z);

        Instantiate(zombiesPrefabs[randomIndexZombies], positionBornZombie, Quaternion.identity);
    }

    void Update()
    {

        float rotationAmount = degreesPerSecond * Time.deltaTime;
        sun.transform.Rotate(Vector3.right, rotationAmount, Space.World);
        
        if(sun.transform.eulerAngles.x >= -5.0 && sun.transform.eulerAngles.x <= 180.0) {
            
            if(isDay)
            {
                Debug.Log("Está de dia - "+day);
                isDay = false;
                day += 1;
            }
            isNight = true;

            //identificar qual é o dia
            //ativar a loja
            //Spawnar poucos zumbis
        } else {
           
            if(isNight)
            {
                Debug.Log("Está de noite");
                isNight = false;
            }
            isDay = true;

            
            //spawnar gradualmente zumbis (de acordo com o dia atual)
            //Desativar loja
        }

        if (Time.time >= nextTime)
        {
            SpawnZombie();

            nextTime += spawnInterval;
        }

        if (!playerController.VerifyHP())
        {
            GameOver();
        }
    }

    private void GameOver() {
        DestroyAllWithTag("Zombie");
        canvasGameOver.transform.Find("XP").GetComponent<TextMeshProUGUI>().text = playerController.GetMoneys().ToString();
        canvasGameOver.gameObject.SetActive(true);
    }

    void DestroyAllWithTag(string tag)
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Demo");
    }

}
