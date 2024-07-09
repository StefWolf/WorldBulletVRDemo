using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// In this state, the NPC is walking to the player
public class StateWalkToPlayer : StateZombieNPC {
    private bool seePlayer;

    public StateWalkToPlayer(ZombieNPC npc) : base(npc) { }

    public override void Enter() {
        Debug.Log("Entrou no estado WalkToPlayer");
    }

    public override void Update() {
        Debug.Log("WalkToPlayer");

        /*
        if (seePlayer) {
            npc.ChangeState(npc.GetStateWalkToPlayer());
        }
        */

        
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