using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// In this state, the NPC is destroyinh the wall
public class StateKillPlayer : StateZombieNPC {
    private bool nextToPlayer;

    public StateKillPlayer(ZombieNPC npc) : base(npc) { }

    public override void Enter() {
        nextToPlayer = true;
        npc.SetAttackAnimation();
        Debug.Log("Entrou no estado KillPlayerl");
    }

    public override void Update() {
        Debug.Log("KillPlayer");

        // TODO - check if it is next to Player
        if (!nextToPlayer) {
            npc.ChangeState(npc.GetStateWalkToPlayer());
        }
    }

    public override void Exit() {
        Debug.Log("Saiu do estado KillPlayer");
    }


    public override void HandleCollision(Collider other) {
        Debug.Log("Colidindo com algo");
    }

    public override void CollisionFinished(Collider other) {
        Debug.Log("Saiu da colisão");
    }


}