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
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
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
        Debug.Log("Player Connected: " + playerSessionId);

        GameObject playerAvatar = Instantiate(playerAvatarPrefab);
        playerAvatar.GetComponent<Player>().playerId = playerSessionId;

        if (playerGameObjects.ContainsKey(playerSessionId)) {
            Debug.LogError("Player with session ID attmempted to connect twice: " + playerSessionId);
        }
        playerGameObjects.Add(playerSessionId, playerAvatar);
    }

    private void HandlePlayerDisconnected(RealmEventBase playerDisconnectedEventData) {
        PlayerDisconnectedEvent playerDisconnectedEvent = playerDisconnectedEventData as PlayerDisconnectedEvent;

        string playerSessionId = playerDisconnectedEvent.playerSessionId;
        Debug.Log("Player Disconnected: " + playerSessionId);

        GameObject playerAvatar;
        if (playerGameObjects.TryGetValue(playerSessionId, out playerAvatar)) {
            Destroy(playerAvatar);
            playerGameObjects.Remove(playerSessionId);
        } else {
            Debug.LogError("Unrecognized Player Disconnected: " + playerSessionId);
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
