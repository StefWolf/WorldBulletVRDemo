using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// In this state, the NPC is walking to the wall
public class StateWalkToWall : StateZombieNPC {
    private bool seePlayer;
    private bool nextToWall;
    private Vector3 directionToWall, directionToPlayer;
    private Quaternion targetRotation;

    public StateWalkToWall(ZombieNPC npc) : base(npc) { }

    public override void Enter() {
        seePlayer = false;
        nextToWall = false;
        npc.SetRunAnimation();
        Debug.Log("Entrou no estado WalkToWall");
    }

    public override void Update() {
        directionToPlayer = npc.GetPlayerPosition() - npc.transform.position;

      //  Debug.Log("StateWalkToWall - Distance: " + directionToPlayer.magnitude);

        // Check if it is seeing the Player
        seePlayer = directionToPlayer.magnitude <= npc.GetMinDistance();
        if (seePlayer) {
            npc.ChangeState(npc.GetStateWalkToPlayer());
        }

        if (nextToWall) {
            npc.ChangeState(npc.GetStateDestroyWall());
        }

        directionToWall = npc.GetWallPosition() - npc.transform.position;
        directionToWall.Normalize();
        targetRotation = Quaternion.LookRotation(directionToWall);
        targetRotation.x = 0;
        targetRotation.z = 0;
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, targetRotation, Time.deltaTime);
        npc.transform.Translate(directionToWall * npc.GetMoveSpeed() * Time.deltaTime, Space.World);
    }

    public override void Exit() {
        Debug.Log("Saiu do estado WalkToWall");
    }


    public override void HandleCollision(Collider other) {
        if (other.CompareTag("PlayerBody") && !seePlayer) {
            seePlayer = true;
        }
        if (other.CompareTag("Gate") && !nextToWall) {
            nextToWall = true;
        }
    }

    public override void CollisionFinished(Collider other) {
        Debug.Log("Saiu da colisão");
    }


}