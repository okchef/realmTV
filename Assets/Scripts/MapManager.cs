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

    void Awake() {
        mapGrid = FindObjectOfType<Grid>();
        tilemap = FindObjectOfType<Tilemap>();
    }

    public static void InitMap(MapState mapState) {
        instance.tilemap.ClearAllTiles();
        foreach (Vector3Int coord in mapState.Coordinates()) {
            HexState hex = mapState.GetHexState(coord);
            if (hex.terrain.Equals("GRASS")) {
                instance.tilemap.SetTile(coord, instance.grassTile);
            } else {
                instance.tilemap.SetTile(coord, instance.waterTile);
            }
        }
        Debug.Log("Map initialized");
    }
}
