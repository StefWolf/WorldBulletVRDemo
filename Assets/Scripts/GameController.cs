using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    private int day, multiplier;
    private int activeZombies = 0;
    private bool isDay = true;
    private bool isNight = true;
    private float degreesPerSecond;
    private float currentSpawnDelay;

    // private float nextTime = 0.0f;
    // private float spawnInterval = 2.0f;

    [SerializeField] private int maxZombies = 20;
    [SerializeField] private float initialSpawnDelay = 5f;
    [SerializeField] private float minimumSpawnDelay = 1f;
    [SerializeField] private float spawnAcceleration = 0.1f;
    [SerializeField] private float minutesForFullRotation = 1.0f;
    [SerializeField] private AudioSource audioGlobal;
    [SerializeField] private Canvas canvasGameOver;
    [SerializeField] private GameObject sun;
    [SerializeField] private GameObject buttonShop;
    [SerializeField] private List<GameObject> pointsOfSpawnZombie;
    [SerializeField] private List<GameObject> zombiesPrefabs;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Gate gate;

    private int limit = 0; // TODO - remover

    void Start() {
        canvasGameOver.gameObject.SetActive(false);
        degreesPerSecond = 360.0f / (minutesForFullRotation * 60.0f);
        day = 0;
        playerController.StartRenderTexts();
        audioGlobal.Play();

        currentSpawnDelay = initialSpawnDelay;
        StartCoroutine(this.SpawnZombiesGradually());
    }

    void Update() {
        float rotationAmount = degreesPerSecond * Time.deltaTime;
        sun.transform.Rotate(Vector3.right, rotationAmount, Space.World);

        if (sun.transform.eulerAngles.x >= -5.0 && sun.transform.eulerAngles.x <= 180.0) {

            if (isDay)  {
                Debug.Log("Está de dia - " + day);
                isDay = false;
                day += 1;
            }
            isNight = true;

            //identificar qual é o dia
            //ativar a loja
            //Spawnar poucos zumbis
        } else  {
            if (isNight)
            {
                Debug.Log("Está de noite");
                isNight = false;
            }
            isDay = true;


            //spawnar gradualmente zumbis (de acordo com o dia atual)
            //Desativar loja
        }

        if (!playerController.VerifyHP())
            GameOver();


        if (gate.GetHp() == 0)
            GameOver();
    }


    private IEnumerator SpawnZombiesGradually() {
        while (true) {
            yield return new WaitForSeconds(currentSpawnDelay);
            multiplier = activeZombies >= maxZombies ? 1 : -1;
            currentSpawnDelay = Mathf.Min(initialSpawnDelay, currentSpawnDelay + (spawnAcceleration*multiplier));
            this.SpawnZombie();
        }
    }

    private void SpawnZombie() {   
        // if (limit > 0) { return; }
        System.Random random = new System.Random();
        int randomIndex = random.Next(0, pointsOfSpawnZombie.Count);
        int randomIndexZombies = random.Next(0, zombiesPrefabs.Count);

        Vector3 positionBornZombie = new Vector3(
            pointsOfSpawnZombie[randomIndex].transform.position.x, 
            pointsOfSpawnZombie[randomIndex].transform.position.y,
            pointsOfSpawnZombie[randomIndex].transform.position.z);

        GameObject zombie = Instantiate(zombiesPrefabs[randomIndexZombies], positionBornZombie, Quaternion.identity);
        // limit += 1;

        activeZombies += 1;

        ZombieNPC zombieScript = zombie.AddComponent<ZombieNPC>();
        zombieScript.SetGameController(this);
    }

    public void DecrementZombieCount() {
        Debug.Log("Decrement Zombie Count");
        activeZombies = Mathf.Max(0, activeZombies - 1);
    }

    private void GameOver() {
        Debug.Log("GAME OVER");
        DestroyAllWithTag("Zombie");
        canvasGameOver.transform.Find("XP").GetComponent<TextMeshProUGUI>().text = playerController.GetMoneys().ToString();
        canvasGameOver.gameObject.SetActive(true);
    }

    void DestroyAllWithTag(string tag) {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
    }

    public void RestartLevel() {
        SceneManager.LoadScene("Demo");
    }

}
