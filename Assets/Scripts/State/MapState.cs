using System;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

[Serializable]
public class MapState : IReadMapState
{
    public Dictionary<string, HexState> hexes = new Dictionary<string, HexState>();

    public Nullable<int> height;

    public Nullable<int> width;

    public Nullable<Vector3Int> spawnPosition;

    public HexState GetHexState(Vector3Int position) {
        return hexes[position.x + "," + position.y];
    }

    public void SetHexState(Vector3Int position, HexState hex) {
        hexes[position.x + "," + position.y] = hex;
    }

    public void UpdateWith(MapState other) {
        foreach (KeyValuePair<string, HexState> element in other.hexes) {
            HexState currentHexState;
            if (this.hexes.TryGetValue(element.Key, out currentHexState)) {
                currentHexState.UpdateWith(element.Value);
            } else {
                this.hexes.Add(element.Key, element.Value);
            }
        }

        if (other.height != null) {
            this.height = other.height;
        }

        if (other.width != null) {
            this.width = other.width;
        }

        if (other.spawnPosition != null)
            this.spawnPosition = other.spawnPosition;
    }

    public List<Vector3Int> Coordinates() {
        List<Vector3Int> coordinates = new List<Vector3Int>();
        foreach (String key in hexes.Keys) {
            String[] coord = key.Split(',');
            coordinates.Add(new Vector3Int(int.Parse(coord[0]), int.Parse(coord[1]), 0));
        }
        return coordinates;
    }

    IReadHexState IReadMapState.GetHexState(Vector2Int position) {
        return hexes[position.x + "," + position.y];
    }

    Vector3Int IReadMapState.GetSpawnPosition() {
        return this.spawnPosition ?? new Vector3Int(-1,-1,-1);
    }
}
