using UnityEngine;
using System;

[Serializable]
public class HexMapCoordinates
{
    private int x;

    private int y;

    public Vector3Int ToVector3Int() {
        return new Vector3Int(x, y, 0);
    }

    public Vector2Int ToVector2Int() {
        return new Vector2Int(x, y);
    }
}