using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// In this state, the NPC is walking to the wall
public class StateWalkToWall : StateZombieNPC {
    private bool seePlayer;
    private bool nextToWall;
    private Vector3 directionToMove;
    private Quaternion targetRotation;

    public StateWalkToWall(ZombieNPC npc) : base(npc) { }

    public override void Enter() {
        seePlayer = false;
        nextToWall = false;
        npc.SetRunAnimation();
        Debug.Log("Entrou no estado WalkToWall");
    }

    public override void Update() {
        if (seePlayer) {
            npc.ChangeState(npc.GetStateWalkToPlayer());
        }
        if (nextToWall) {
            npc.ChangeState(npc.GetStateDestroyWall());
        }

        directionToMove = npc.GetWallPosition() - npc.transform.position;
        directionToMove.Normalize();
        targetRotation = Quaternion.LookRotation(directionToMove);
        targetRotation.x = 0;
        targetRotation.z = 0;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, targetRotation, Time.deltaTime);
        npc.transform.Translate(directionToMove * npc.GetMoveSpeed() * Time.deltaTime, Space.World);
    }

    public override void Exit() {
        Debug.Log("Saiu do estado WalkToWall");
    }


    public override void HandleCollision(Collider other) {
        if (other.CompareTag("PlayerBody") && !seePlayer) {
            // TODO - seePlayer = true;
            nextToWall = true;
        }
        if (other.CompareTag("Gate") && !nextToWall) {
            nextToWall = true;
        }
    }

    public override void CollisionFinished(Collider other) {
        Debug.Log("Saiu da colisão");
    }


}