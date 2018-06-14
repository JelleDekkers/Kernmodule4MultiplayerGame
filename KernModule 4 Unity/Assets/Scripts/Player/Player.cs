using System;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private PlayerCharacter characterPrefab;

    [SyncVar] public new string name = "New Player";
    [SyncVar] public int id = -1;
    [SyncVar] public int kills;
    [SyncVar] public int deaths;

    public PlayerCharacter character;
    public delegate int NewPlayerDelegate(Player player);
    public static NewPlayerDelegate OnNewPlayer;
    public static Action<Player> OnLocalPlayerCreated;
    public TurnManager turnManagerPrefab;

    private Action OnPlayerCharacterDeath;

    private void Start() {
        OnPlayerCharacterDeath = delegate () {
            TurnManager.onNewTurn += RespawnOnTurn;
        };
    }

    public override void OnStartClient() {
        base.OnStartLocalPlayer();
        if (OnNewPlayer != null)
            id = OnNewPlayer.Invoke(this);
    }

    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
        if(OnLocalPlayerCreated != null)
            OnLocalPlayerCreated.Invoke(this);
        TurnManager.Instance.SetLocalPlayer(GetComponent<NetworkIdentity>(), id);
    }

    public override void OnStartAuthority() {
        base.OnStartAuthority();
        gameObject.name += " (LOCAL)";
    }

    public override void OnStartServer() {
        base.OnStartServer();
    }

    private void OnDestroy() {
        TurnManager.Instance.RemovePlayer(this);
    }

    public void SpawnCharacterObject() {
        character = Instantiate(characterPrefab, Vector3.zero + Vector3.up, Quaternion.identity) as PlayerCharacter;
        NetworkServer.SpawnWithClientAuthority(character.gameObject, connectionToClient);
        character.RpcInit(id);
        character.onDeath += OnPlayerCharacterDeath;
    }

    private void RespawnOnTurn(int playerID, int turnNr) {
        character.onDeath -= OnPlayerCharacterDeath;
        if (playerID == id) {
            SpawnCharacterObject();
            TurnManager.onNewTurn -= RespawnOnTurn;
        }
    }

    private void OnGUI() {
        if (isLocalPlayer && TurnManager.Instance.gameStarted == false) {
            GUILayout.BeginArea(new Rect(10, 300, 200, 100));
            GUILayout.BeginVertical("box", GUILayout.Width(200));
            GUILayout.Label("Enter Name");
            name = GUILayout.TextField(name);
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
