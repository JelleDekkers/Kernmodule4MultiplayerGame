using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTurnHandler : MonoBehaviour {

    public Projectile projectile;

    private void Start() {
        TurnManager.onNewTurn += projectile.OnNewTurn;
        TurnManager.onTurnEnd += projectile.OnTurnEnded;
    }

    private void OnDestroy() {
        TurnManager.onNewTurn -= projectile.OnNewTurn;
        TurnManager.onTurnEnd -= projectile.OnTurnEnded;
    }
}
