using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterTurnHandler : NetworkBehaviour, IPlayerOwnedObject {

    public Player Owner { get; private set; }
    public Player owner;

    private CustomCharacterController characterController;
    private Inventory inventory;

    public void InitOwnership(Player player) {
        Owner = player;
        characterController = GetComponent<CustomCharacterController>();
        inventory = GetComponent<Inventory>();

        if (isLocalPlayer)
            EnableComponents(TurnManager.isMyTurn);
        else
            EnableComponents(false);
    }

    public override void OnStartAuthority() {
        base.OnStartAuthority();
        TurnManager.onNewTurn += OnNewTurn;
        TurnManager.onTurnEnd += OnTurnEnded;
    }

    private void OnNewTurn(int turnNr, int playerID) {
        if (Owner.id == playerID)
            OnMyTurnStarted();
    }

    private void OnMyTurnStarted() {
        EnableComponents(true);
    }

    private void OnTurnEnded() {
        EnableComponents(false);
    }

    private void EnableComponents(bool enable) {
        characterController.enabled = enable;
        inventory.enabled = enable;
    }

    private void OnDestroy() {
        TurnManager.onNewTurn -= OnNewTurn;
        TurnManager.onTurnEnd -= OnTurnEnded;
    }
}
