using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// In this state, the NPC is walking to the player
public class StateWalkToPlayer : StateZombieNPC {
    private bool seePlayer;
    private bool nextToPlayer;
    private float minDistance = 1f;
    private Vector3 directionToMove;
    private Quaternion targetRotation;

    public StateWalkToPlayer(ZombieNPC npc) : base(npc) { }

    public override void Enter() {
        seePlayer = true;
        nextToPlayer = false;
        npc.SetWalkingAnimation();
        Debug.Log("Entrou no estado WalkToPlayer");
    }

    public override void Update() {
        Debug.Log("WalkToPlayer");

        if (!seePlayer) {
            npc.ChangeState(npc.GetStateWalkToWall());
        }

        directionToMove = npc.GetPlayerPosition() - npc.transform.position;
        directionToMove.Normalize();

        // TODO - check if it is next to Player
        nextToPlayer = directionToMove.sqrMagnitude < minDistance;
        if (nextToPlayer) {
            npc.ChangeState(npc.GetStateKillPlayer());
        }

        targetRotation = Quaternion.LookRotation(directionToMove);
        targetRotation.x = 0;
        targetRotation.z = 0;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, targetRotation, Time.deltaTime);
        npc.transform.Translate(directionToMove * npc.GetMoveSpeed() * Time.deltaTime, Space.World);
    }

    public override void Exit() {
        Debug.Log("Saiu do estado WalkToPlayer");
    }


    public override void HandleCollision(Collider other) {
        Debug.Log("Colidindo com algo");
    }

    public override void CollisionFinished(Collider other) {
        Debug.Log("Saiu da colisão");
    }


}