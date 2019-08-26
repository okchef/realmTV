using UnityEngine;
public interface IReadMapState
{
    Vector3Int GetSpawnPosition();

    IReadHexState GetHexState(Vector2Int position);
}
