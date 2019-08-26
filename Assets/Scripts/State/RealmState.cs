using System.Collections.Generic;
using System;

[Serializable]
public class RealmState : IReadRealmState {
    public String gameId;

    public Dictionary<String, PlayerState> players = new Dictionary<string, PlayerState>();

    public MapState map;

    public void UpdateWith(RealmState other) {
        foreach (KeyValuePair<String, PlayerState> element in other.players) {
            PlayerState currentPlayerState;
            if (this.players.TryGetValue(element.Key, out currentPlayerState)) {
                currentPlayerState.UpdateWith(element.Value);
            } else {
                this.players.Add(element.Key, element.Value);
            }
        }

        if (this.map == null) {
            this.map = new MapState();
        }

        if (other.map != null) {
            map.UpdateWith(other.map);
        }
    }

    IReadPlayerState IReadRealmState.GetPlayerState(string playerId) {
        return this.players[playerId];
    }

    public IReadMapState GetMapState() {
        return this.map;
    }
}
