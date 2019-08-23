using UnityEngine;
using System.Collections.Generic;
public class PlayerManager : MonoBehaviour {

    public GameObject playerAvatarPrefab;

    private Dictionary<string, GameObject> playerGameObjects = new Dictionary<string, GameObject>(); 

    private static PlayerManager playerManager;

    public static PlayerManager instance {
        get {
            if (!playerManager) {
                playerManager = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;

                if (!playerManager) {
                    Debug.LogError("There needs to be one active PlayerManager script on a GameObject in your scene.");
                } else {
                    playerManager.Init();
                }
            }

            return playerManager;
        }
    }

    private void Init() {

    }

    public void OnEnable() {
        EventManager.StartListening<PlayerConnectedEvent>(HandlePlayerConnected);
        EventManager.StartListening<PlayerDisconnectedEvent>(HandlePlayerDisconnected);
        EventManager.StartListening<PlayerMoveEvent>(HandlePlayerMove);
    }

    public void OnDisable() {
        EventManager.StopListening<PlayerConnectedEvent>(HandlePlayerConnected);
        EventManager.StopListening<PlayerDisconnectedEvent>(HandlePlayerDisconnected);
        EventManager.StartListening<PlayerMoveEvent>(HandlePlayerMove);
    }

    private void HandlePlayerMove(RealmEventBase baseEvent) {
        PlayerMoveEvent playerMoveEvent = baseEvent as PlayerMoveEvent;
        Debug.Log("Player Moved: " + playerMoveEvent.direction);
    }

    private void HandlePlayerConnected(RealmEventBase playerConnectedEventData) {
        PlayerConnectedEvent playerConnectedEvent = playerConnectedEventData as PlayerConnectedEvent;

        string playerSessionId = playerConnectedEvent.playerSessionId;
        string playerId = playerConnectedEvent.playerId;
        Debug.Log("Player Connected {playerId: " + playerId + ", playerSessionId: " + playerSessionId);

        GameObject playerAvatar = Instantiate(playerAvatarPrefab);
        playerAvatar.GetComponent<Player>().playerId = playerId;

        IReadPlayerState playerState = RealmStateManager.GetRealmState().GetPlayerState(playerId);
        Color playerColor = new Color();

        if (ColorUtility.TryParseHtmlString(playerState.GetColor(), out playerColor)) {
            playerAvatar.GetComponent<MeshRenderer>().material.color = playerColor;
        }

        if (playerGameObjects.ContainsKey(playerId)) {
            Debug.LogError("Player with ID attmempted to connect twice: " + playerId);
        }
        playerGameObjects.Add(playerId, playerAvatar);
    }

    private void HandlePlayerDisconnected(RealmEventBase playerDisconnectedEventData) {
        PlayerDisconnectedEvent playerDisconnectedEvent = playerDisconnectedEventData as PlayerDisconnectedEvent;

        string playerSessionId = playerDisconnectedEvent.playerSessionId;
        string playerId = playerDisconnectedEvent.playerId;
        Debug.Log("Player Disconnected {playerId: " + playerId + ", playerSessionId: " + playerSessionId);

        GameObject playerAvatar;
        if (playerGameObjects.TryGetValue(playerId, out playerAvatar)) {
            Destroy(playerAvatar);
            playerGameObjects.Remove(playerId);
        } else {
            Debug.LogError("Unrecognized Player Disconnected: " + playerId);
        }
    }

    public static GameObject GetPlayerAvatar(string playerSessionId) {
        GameObject playerAvatar;
        if (!instance.playerGameObjects.TryGetValue(playerSessionId, out playerAvatar)) {
            Debug.LogError("Could not retrieve Player Avatar for playerSessionId: " + playerSessionId);
        }
        return playerAvatar;
    }
}
