using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// In this state, the NPC is walking to the player
public class StateWalkToPlayer : StateZombieNPC {
    private bool seePlayer;
    private bool nextToPlayer;
    private Vector3 directionToPlayer;
    private Quaternion targetRotation;

    public StateWalkToPlayer(ZombieNPC npc) : base(npc) { }

    public override void Enter() {
        seePlayer = true;
        nextToPlayer = false;
        npc.SetRunAnimation();
        Debug.Log("Entrou no estado WalkToPlayer");
    }

    public override void Update() {
        directionToPlayer = npc.GetPlayerPosition() - npc.transform.position;

     //   Debug.Log("StateWalkToPlayer - Distance: " + directionToPlayer.magnitude);

        nextToPlayer = directionToPlayer.magnitude <= 2.5f;
        if (nextToPlayer) {
            npc.ChangeState(npc.GetStateKillPlayer());
        }

        seePlayer = directionToPlayer.magnitude <= npc.GetMinDistance();
        if (!seePlayer) {
           npc.ChangeState(npc.GetStateWalkToWall());
        }

        directionToPlayer.Normalize();
        targetRotation = Quaternion.LookRotation(directionToPlayer);
        targetRotation.x = 0;
        targetRotation.z = 0;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, targetRotation, Time.deltaTime);
        npc.transform.Translate(directionToPlayer * npc.GetMoveSpeed() * Time.deltaTime, Space.World);
    }

    public override void Exit() {
        Debug.Log("Saiu do estado WalkToPlayer");
    }


    public override void HandleCollision(Collider other) {
        Debug.Log("Colidindo com algo");
        if (other.CompareTag("PlayerBody") && !nextToPlayer) {
            nextToPlayer = true;
        }
    }

    public override void CollisionFinished(Collider other) {
        Debug.Log("Saiu da colisão");
    }


}