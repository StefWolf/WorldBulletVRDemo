using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// In this state, the NPC is destroyinh the wall
public class StateDestroyWall : StateZombieNPC {
    private bool seePlayer;
    private bool nextToWall;
    private Vector3 directionToPlayer;

    public StateDestroyWall(ZombieNPC npc) : base(npc) { }

    public override void Enter() {
        seePlayer = false;
        nextToWall = true;
        npc.SetAttackAnimation();
        Debug.Log("Entrou no estado DestroyWall");
    }

    public override void Update() {
        Debug.Log("DestroyWall");
        
        directionToPlayer = npc.GetPlayerPosition() - npc.transform.position;

        seePlayer = directionToPlayer.magnitude <= npc.GetMinDistance();
        if (seePlayer) {
            npc.ChangeState(npc.GetStateWalkToPlayer());
        }

        if (!nextToWall) {
            npc.ChangeState(npc.GetStateWalkToWall());
        }
    }

    public override void Exit() {
        Debug.Log("Saiu do estado DestroyWall");
    }


    public override void HandleCollision(Collider other) {
        Debug.Log("Colidindo com algo");
    }

    public override void CollisionFinished(Collider other) {
        Debug.Log("Saiu da colisão");
    }


}