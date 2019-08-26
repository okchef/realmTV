using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Grid mapGrid;

    public Tilemap tilemap;

    public Tile grassTile;

    public Tile waterTile;

    public Tile blackTile;

    private static MapManager mapManager;

    public static MapManager instance {
        get {
            if (!mapManager) {
                mapManager = FindObjectOfType(typeof(MapManager)) as MapManager;

                if (!mapManager) {
                    Debug.LogError("There needs to be one active MapManager script on a GameObject in your scene.");
                }
            }

            return mapManager;
        }
    }

    public void OnEnable() {
        mapGrid = FindObjectOfType<Grid>();
        tilemap = FindObjectOfType<Tilemap>();
        EventManager.StartListening<PlayerMoveEvent>(HandlePlayerMove);
    }

    public void OnDisable() {
        mapGrid = null;
        tilemap = null;
        EventManager.StartListening<PlayerMoveEvent>(HandlePlayerMove);
    }

    public static void InitMap(MapState mapState) {
        instance.tilemap.ClearAllTiles();
        foreach (Vector3Int coord in mapState.Coordinates()) {
            HexState hex = mapState.GetHexState(coord);
            if (!hex.visible) {
                instance.tilemap.SetTile(coord, instance.blackTile);
            } else if (hex.terrain.Equals("GRASS")) {
                instance.tilemap.SetTile(coord, instance.grassTile);
            } else {
                instance.tilemap.SetTile(coord, instance.waterTile);
            }
        }
        Debug.Log("Map initialized");
    }

    private void HandlePlayerMove(RealmEventBase baseEvent) {
        PlayerMoveEvent playerMoveEvent = baseEvent as PlayerMoveEvent;
        Vector2Int playerPosition = RealmStateManager.GetRealmState().GetPlayerState(playerMoveEvent.playerId).GetPosition();
        IReadHexState hexState = RealmStateManager.GetRealmState().GetMapState().GetHexState(playerPosition);
        if (hexState.IsVisible()) {
            if (hexState.GetTerrain().Equals("GRASS")) {
                instance.tilemap.SetTile((Vector3Int)playerPosition, instance.grassTile);
            } else {
                instance.tilemap.SetTile((Vector3Int)playerPosition, instance.waterTile);
            }
        }
    }
}
