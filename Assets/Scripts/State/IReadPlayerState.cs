using UnityEngine;

public interface IReadPlayerState
{
    bool IsConnected();

    Vector2Int GetPosition();

    string GetPlayerSessionId();

    string GetPlayerId();

    string GetPlayerName();

    string GetColor();
}
