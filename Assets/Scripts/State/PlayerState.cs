using System;
using UnityEngine;

[Serializable]
public class PlayerState
{
    public bool connected;

    public HexMapCoordinates position;

    public String playerSessionId;

    public String playerId;

    public String playerName;

    public void UpdateWith(PlayerState other) {
        this.connected = other.connected;
        this.position = other.position;
        this.playerName = other.playerName;
        this.playerSessionId = other.playerSessionId;
        Debug.Assert(this.playerId.Equals(other.playerId));
    }
}