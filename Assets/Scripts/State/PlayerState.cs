using System;
using UnityEngine;

[Serializable]
public class PlayerState : IReadPlayerState
{
    public bool connected;

    public Vector2Int position;

    public string playerSessionId;

    public string playerId;

    public string playerName;

    public string color;

    public void UpdateWith(PlayerState other) {
        this.connected = other.connected;
        this.position = other.position;
        this.playerName = other.playerName;
        this.playerSessionId = other.playerSessionId;
        this.color = other.color;
        Debug.Assert(this.playerId.Equals(other.playerId));
    }

    string IReadPlayerState.GetColor() {
        return color;
    }

    string IReadPlayerState.GetPlayerId() {
        return playerId;
    }

    string IReadPlayerState.GetPlayerName() {
        return playerName;
    }

    string IReadPlayerState.GetPlayerSessionId() {
        return playerSessionId;
    }

    Vector2Int IReadPlayerState.GetPosition() {
        return position;
    }

    bool IReadPlayerState.IsConnected() {
        return connected;
    }
}