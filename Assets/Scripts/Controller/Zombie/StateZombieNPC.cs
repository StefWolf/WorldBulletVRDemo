using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StateZombieNPC {
    protected ZombieNPC npc;

    public StateZombieNPC(ZombieNPC npc) {
        this.npc = npc;
    }

    public abstract void HandleCollision(Collider other);

    public abstract void CollisionFinished(Collider other);

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
