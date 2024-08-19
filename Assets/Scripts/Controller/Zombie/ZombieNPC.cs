using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieNPC : MonoBehaviour {
    private int maxLife = 100;
    private int life = 100;
    private float moveSpeedMemo, minDistance = 10f;
    private Animator animator;
    private GameController gameController;

    // NPC finite state machine
    private StateZombieNPC currentState;
    private StateDestroyWall stateDestroyWall;
    private StateWalkToPlayer stateWalkToPlayer;
    private StateWalkToWall stateWalkToWall;
    private StateKillPlayer stateKillPlayer;

    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<AudioClip> sounds;
    [SerializeField] private AudioClip shotReactionAudio;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject wall;
    [SerializeField] private ParticleSystem particleDamage;

    private float timeSinceLastDamage = 0f;
    private float damageInterval = 2f;

    private void Start() {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        wall = GameObject.FindWithTag("Gate");

        stateDestroyWall = new StateDestroyWall(this);
        stateWalkToPlayer = new StateWalkToPlayer(this);
        stateWalkToWall = new StateWalkToWall(this);
        stateKillPlayer = new StateKillPlayer(this);
        
        moveSpeedMemo = moveSpeed;
        currentState = stateWalkToWall;
        currentState.Enter();

       // this.PlaySound();
    }

    private void Update() {
        currentState.Update();
        if(life <= 0) {
            Destroy(gameObject);
            gameController.DecrementZombieCount();
        }

        timeSinceLastDamage += Time.deltaTime;
    }

    private void PlaySound() {
        AudioManager.Instance.StopAudio();
        System.Random random = new System.Random();
        int index = random.Next(0, sounds.Count);
        AudioManager.Instance.PlayAudio(sounds[index], true);
    }

    public void ChangeState(StateZombieNPC newState) {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void AddToLife(int life) {
        this.life += life;
        if (this.life > maxLife) {
            this.life = maxLife;
        }
        if (this.life < 0) {
            Destroy(gameObject);
            gameController.DecrementZombieCount();
        }
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log("o " + other.tag + " entrou em colisão com o zumbi");
        if (other.CompareTag("Bullet")){
            Debug.Log("Acertou o zumbi!");
            // AudioManager.Instance.StopAudio();
            moveSpeed = 0;
            this.SetShotReactionAnimation(true);
            particleDamage.Play();
            this.SetLife(life - 20);
            PlayerController play = player.gameObject.GetComponent<PlayerController>();
            play.AddMoney(130);
            Debug.Log("Life atual: " + life);
            Invoke("FinishShotReactionAnimation", 0.7f);
        }
    }

    private void FinishShotReactionAnimation()
    {
        this.SetShotReactionAnimation(false);
        moveSpeed = moveSpeedMemo;
    }

    public void OnTriggerStay(Collider other) {

        if (other.CompareTag("PlayerBody"))
        {
            if (timeSinceLastDamage >= damageInterval)
            {
                Debug.Log("Player perdendo vida");
                PlayerController player = other.GetComponentInParent<PlayerController>();
                if (player != null)
                {
                    player.RemoveHP(10);
                }

                // Reinicia o contador de tempo
                timeSinceLastDamage = 0f;
            }
        }

        currentState.HandleCollision(other);
}

    public void OnTriggerExit(Collider other) {
        currentState.CollisionFinished(other);
    }

    // Getters and Setters
    public StateDestroyWall GetStateDestroyWall(){
        return this.stateDestroyWall;
    }
    public StateWalkToPlayer GetStateWalkToPlayer(){
        return this.stateWalkToPlayer;
    }
    public StateWalkToWall GetStateWalkToWall(){
        return this.stateWalkToWall;
    }
    public StateKillPlayer GetStateKillPlayer() {
        return this.stateKillPlayer;
    }

    public int GetLife() {
        return this.life;
    }
    public float GetMinDistance() {
        return this.minDistance;
    }

    public float GetMoveSpeed() {
        return this.moveSpeed;
    }

    public void SetGameController(GameController gameController) {
        this.gameController = gameController;
    }

    public void SetWalkingAnimation()  {
        this.animator.SetBool("attack", false);
        this.animator.SetBool("run", false);
        this.animator.SetBool("walk", true);
    }

    public void SetRunAnimation() {
        this.animator.SetBool("attack", false);
        this.animator.SetBool("run", true);
    }

    public void SetAttackAnimation() {
        this.animator.SetBool("attack", true);
    }

    public void SetShotReactionAnimation(bool flag) {
        Debug.Log("Shot Reaction End!");
        AudioManager.Instance.PlayAudio(shotReactionAudio);
        this.animator.SetBool("shotReaction", flag);
        this.PlaySound();
    }

    public void SetLife(int life){
        if (life > maxLife){
            this.life = maxLife;
        } else if (life < 0) {
            this.life = 0;
        } else {
            this.life = life;
        }
    }

    public Vector3 GetPlayerPosition(){
        if (player != null) {
            return player.transform.position;
        }
        return Vector3.zero;
    }

    public Vector3 GetWallPosition(){
        if (wall != null) {
            return wall.transform.position;
        }
        return Vector3.zero;
    }

    public int GetDamageAmount(){
        return damageAmount;
    }
}